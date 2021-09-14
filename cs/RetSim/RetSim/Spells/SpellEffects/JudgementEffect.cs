﻿using RetSim.Simulation;
using RetSim.Simulation.Events;
using RetSim.Units.Player.State;

namespace RetSim.Spells.SpellEffects;

class JudgementEffect : SpellEffect
{
    public override ProcMask Resolve(FightSimulation fight, SpellState state)
    {
        if (fight.Player.Auras.CurrentSeal != null)
        {
            var seal = fight.Player.Auras.CurrentSeal;

            fight.Player.Auras.Cancel(seal, fight.Player, fight.Player, fight);

            fight.Queue.Add(new CastEvent(seal.Judgement, fight.Player, fight.Enemy, fight, fight.Timestamp));
        }

        return ProcMask.None;
    }
}