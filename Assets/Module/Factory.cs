using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game;
using Module.Game.Battle;
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
        #region Block
        private static Dictionary<(int job, int index, int level), Block> BlockBuffer { get; set; } =
            new();

        public static Block GetBlock((int job, int index, int level) key)
        {
            var job = ResourceManager.Instance.Jobs[key.job];
            var sprite = ResourceManager.Instance.BlockSpriteDict[((JobType) key.job, key.index)];
            var block = (JobType) key.job switch
            {
                JobType.Warrior =>  new Block(11, $"Sword Attack", 5, SkillType.Attack, TargetType.Enemy, job.Color, sprite),
                JobType.Archer =>  new Block(21, $"Shot", 5, SkillType.Attack, TargetType.Enemy, job.Color, sprite),
                JobType.Wizard =>  new Block(31, $"Magic Shower", 5, SkillType.Attack, TargetType.AllEnemy, job.Color, sprite),
                JobType.Knight =>  new Block(41, $"Heal", 5, SkillType.Heal, TargetType.Unit, job.Color, sprite),
                JobType.Priest =>  new Block(51, $"Heal", 5, SkillType.Heal, TargetType.Unit, job.Color, sprite),
                _ => null
            };
            
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