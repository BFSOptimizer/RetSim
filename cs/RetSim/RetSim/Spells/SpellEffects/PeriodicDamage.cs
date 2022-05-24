using Newtonsoft.Json;
using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.Events;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;
using System.Linq;

namespace RetSim.Spells.SpellEffects;

// todo: Implement "can Crit" functionality for periodic effects"
public class PeriodicDamage : SpellEffect
{
    [JsonIgnore]
    public Spell Spell { get; private set; }
    public int SpellID { get; init; }
    public int Ticks { get; init; }
    public int Interval { get; init; }

    public bool CanCrit { get; init; } = false;

    public School School { get; init; }
    public DefenseType DefenseCategory { get; init; }

    public ProcMask OnCast { get; init; }
    public ProcMask OnHit { get; init; }

    public PeriodicDamage(float value, float dieSides, int spellID, int ticks, int interval, School school, DefenseType defense, ProcMask onCast, ProcMask onHit, bool canCrit = false) : base(value, dieSides)
    {
        School = school;
        SpellID = spellID;
        Ticks = ticks;
        Interval = interval;
        DefenseCategory = defense;
        OnCast = onCast;
        OnHit = onHit;
        CanCrit = canCrit;
    }

    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        if (Spell == null)
            Spell = Data.Collections.Spells[SpellID];

        ProcMask mask = OnCast;

        float miss = Attack.GetMissChance(fight.Player, DefenseCategory, fight.Enemy.Stats[StatName.IncreasedAttackerHitChance].Value);

        if (miss < 0)
            miss = 0;

        var attack = Attack.GetAttackResult(DefenseCategory, miss, 0, 0);

        if (attack.Attack == AttackResult.Hit)
        {
            mask |= OnHit;
        }

        SpellEffect dotEffect = Spell.Effects.First(x => x.GetType() == typeof(Damage));

        SpellState dotState = fight.Player.Spellbook[Spell.ID];

        fight.Queue.Add(new DoTEvent(dotEffect, dotState, fight.Player, fight.Enemy, fight, Ticks, Interval, fight.Timestamp + Interval));

        var entry = new DebuffEntry()
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = Spell.Name,
            Stacks = 0,
            Type = AuraChangeType.Gain
        };

        fight.CombatLog.Add(entry);

        return mask;
    }
}