﻿using RetSim.Items;
using RetSimDesktop.Model;
using System.Collections.Generic;
using static RetSim.Data.Items;

namespace RetSimDesktop.ViewModel
{
    public class RetSimUIModel
    {
        private SelectedGear _SelectedGear;
        private SelectedTalents _SelectedTalents;
        private SimOutput _CurrentSimOutput;
        private SelectedPhases _SelectedPhases;
        private Dictionary<Slot, Dictionary<int, List<EquippableItem>>> _GearByPhases;
        private Dictionary<WeaponType, Dictionary<int, List<EquippableWeapon>>> _WeaponsByPhases;

        public RetSimUIModel()
        {
            Gem strength = Gems[24027];
            Gem crit = Gems[24058];
            Gem stamina = Gems[24054];

            _SelectedGear = new SelectedGear
            {
                SelectedHead = EquippableItem.GetItemWithGems(29073, new Gem[] { MetaGems[32409], strength }),
                SelectedNeck = EquippableItem.GetItemWithGems(29381, null),
                SelectedShoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { strength, crit }),
                SelectedBack = EquippableItem.GetItemWithGems(24259, new Gem[] { strength }),
                SelectedChest = EquippableItem.GetItemWithGems(29071, new Gem[] { strength, strength, strength }),
                SelectedWrists = EquippableItem.GetItemWithGems(28795, new Gem[] { strength, stamina }),
                SelectedHands = EquippableItem.GetItemWithGems(30644, null),
                SelectedWaist = EquippableItem.GetItemWithGems(28779, new Gem[] { strength, stamina }),
                SelectedLegs = EquippableItem.GetItemWithGems(31544, null),
                SelectedFeet = EquippableItem.GetItemWithGems(28608, new Gem[] { strength, crit }),
                SelectedFinger1 = EquippableItem.GetItemWithGems(30834, null),
                SelectedFinger2 = EquippableItem.GetItemWithGems(28757, null),
                SelectedTrinket1 = EquippableItem.GetItemWithGems(29383, null),
                SelectedTrinket2 = EquippableItem.GetItemWithGems(28830, null),
                SelectedRelic = EquippableItem.GetItemWithGems(27484, null),
                SelectedWeapon = Weapons[28429],
            };

            _CurrentSimOutput = new SimOutput() { Progress = 0, DPS = 0, Min = 0, Max = 0, MedianCombatLog = new(), MaxCombatLog = new(), MinCombatLog = new() };

            _SelectedTalents = new SelectedTalents()
            {
                ConvictionEnabled = true,
                CrusadeEnabled = true,
                DivineStrengthEnabled = true,
                FanaticismEnabled = true,
                ImprovedSanctityAuraEnabled = true,
                PrecisionEnabled = true,
                SanctifiedSealsEnabled = true,
                SanctityAuraEnabled = true,
                TwoHandedWeaponSpecializationEnabled = true,
                VengeanceEnabled = true
            };

            _SelectedPhases = new SelectedPhases()
            {
                Phase1Selected = true,
                Phase2Selected = true,
                Phase3Selected = false,
                Phase4Selected = false,
                Phase5Selected = false
            };

            _GearByPhases = new();
            _WeaponsByPhases = new();
            foreach (var item in AllItems.Values)
            {
                if (item is EquippableWeapon weapon)
                {
                    if (!_WeaponsByPhases.ContainsKey(weapon.Type))
                    {
                        _WeaponsByPhases[weapon.Type] = new();
                    }
                    if (!_WeaponsByPhases[weapon.Type].ContainsKey(weapon.Phase))
                    {
                        _WeaponsByPhases[weapon.Type][weapon.Phase] = new();
                    }
                    _WeaponsByPhases[weapon.Type][weapon.Phase].Add(weapon);
                }
                else
                {
                    if (!_GearByPhases.ContainsKey(item.Slot))
                    {
                        _GearByPhases[item.Slot] = new();
                    }

                    if (!_GearByPhases[item.Slot].ContainsKey(item.Phase))
                    {
                        _GearByPhases[item.Slot][item.Phase] = new();
                    }

                    _GearByPhases[item.Slot][item.Phase].Add(item);
                }
            }
        }

        public Dictionary<WeaponType, Dictionary<int, List<EquippableWeapon>>> WeaponsByPhases
        {
            get { return _WeaponsByPhases; }
            set { _WeaponsByPhases = value; }
        }
        public Dictionary<Slot, Dictionary<int, List<EquippableItem>>> GearByPhases
        {
            get { return _GearByPhases; }
            set { _GearByPhases = value; }
        }

        public SelectedGear SelectedGear
        {
            get { return _SelectedGear; }
            set { _SelectedGear = value; }
        }

        public SimOutput CurrentSimOutput
        {
            get { return _CurrentSimOutput; }
            set { _CurrentSimOutput = value; }
        }

        public SelectedTalents SelectedTalents
        {
            get { return _SelectedTalents; }
            set { _SelectedTalents = value; }
        }

        public SelectedPhases SelectedPhases
        {
            get { return _SelectedPhases; }
            set { _SelectedPhases = value; }
        }
    }
}
