using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Game.Blocks;
using Infra.Model.Game.Class;
using Infra.Model.Resource;
using Module.Game;
using Module.Game.Event;
using UnityEngine;
using Enemy = Module.Game.Enemy;
using EventType = Infra.Model.Resource.EventType;
using Unit = Infra.Model.Game.Unit;

namespace Module
{
    /// <summary>
    /// Factory that create object of every kind that is needed to game.
    /// </summary>
    internal class Factory
    {
        #region Class
        private static Dictionary<int, Unit> UnitBuffer { get; set; } =
            new();
        public static Unit GetUnit(int key)
        {
            Unit unit = null;
            if (UnitBuffer.ContainsKey(key)) unit = UnitBuffer[key];
            else
            {
                switch ((JobType) key)
                {
                    case JobType.Warrior:
                        unit = new Warrior();
                        break;
                    case JobType.Archer:
                        unit = new Archer();
                        break;
                    case JobType.Wizard:
                        unit = new Wizard();
                        break;
                    case JobType.Knight:
                        unit = new Knight();
                        break;
                    case JobType.Priest:
                        unit = new Priest();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                UnitBuffer.Add(key, unit);
            }
            
            return unit;
        }
        

        #endregion
        #region Block
        private static Dictionary<(int job, int index, int level), Block> BlockBuffer { get; set; } =
            new();
        public static Block GetBlock((int job, int index, int level) key)
        {
            Block block = null;
            // if (BlockBuffer.ContainsKey(key)) block = BlockBuffer[key];
            // else
            {
                switch ((JobType) key.job)
                {
                    case JobType.Warrior:
                        block = new WarriorBlock01();
                        break;
                    case JobType.Archer:
                        block = new ArcherBlock01();
                        break;
                    case JobType.Wizard:
                        block = new WizardBlock01();
                        break;
                    case JobType.Knight:
                        block = new KnightBlock01();
                        break;
                    case JobType.Priest:
                        block = new PriestBlock01();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                var job = ResourceManager.Instance.Jobs[key.job];
                var sprite = ResourceManager.Instance.BlockSprites[0];
                block.Set($"{job.Name}블럭", job.Color, sprite);
                //BlockBuffer.Add(key, block);
            }
            
            return block;
        }
        #endregion

        #region Event
        private static Dictionary<(SpotEventType type, int index), IEventItem> EventBuffer { get; set; } = new();

        public static IEventItem EventFactory(UIGame uiGame, SpotEvent spotEvent)
        {
            (SpotEventType type, int index) key = (spotEvent.Type, spotEvent.Index);
            IEventItem eventItem = null;

            // if (EventBuffer.ContainsKey(key))
            // {
            //     eventItem = EventBuffer[key];
            //     eventItem.UpdateUIGame(uiGame);
            // }
            // else
            {
                switch (key.type)
                {
                    case SpotEventType.Battle:
                        eventItem = new CommonBattle(uiGame);
                        break;
                    case SpotEventType.Event:
                        switch ((EventType) key.index)
                        {
                            case EventType.GetArtefact:
                                eventItem = new GetArtefact(uiGame);
                                break;
                            case EventType.HealingLake:
                                eventItem = new HealingLake(uiGame);
                                break;
                            case EventType.None:
                            default: break;
                        }

                        break;
                    case SpotEventType.Elite:
                    case SpotEventType.Boss:
                    case SpotEventType.None:
                    case SpotEventType.Rest:
                    case SpotEventType.Artefact:
                    default:
                        throw new NotImplementedException("구현하지 않음");
                        break;
                }
               // EventBuffer[key] = eventItem;
            }
            
            return eventItem;
        }

        #endregion

        #region Enemy

        private static Dictionary<int, Enemy> enemyBuffer { get; set; } = new ();

        public static Enemy EnemyFactory(int key)
        {
            Enemy enemy = null;
            if (enemyBuffer.ContainsKey(key)) enemy = enemyBuffer[key];
            else
            {
                switch ((EnemyType)key)
                {
                    case EnemyType.TestMonster:
                        enemy = new TestEnemy(HowToTarget.First);
                        break;
                    default: break;
                }
            }
            
            return enemy;
        }

        #endregion
    }
}