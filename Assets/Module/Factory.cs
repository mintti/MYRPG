using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game;
using Module.Game.Event;
using Enemy = Module.Game.Enemy;

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
            Block block;
            if (BlockBuffer.ContainsKey(key)) block = (Block)BlockBuffer[key].Clone();
            else
            {
                block = new();
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

            if (EventBuffer.ContainsKey(key)) eventItem = EventBuffer[key];
            else
            {
                switch (key.type)
                {
                    case SpotEventType.Battle:
                        switch ((BattleType) key.index)
                        {
                            case BattleType.Common:
                                eventItem = new CommonBattle(uiGame);
                                break;
                            case BattleType.Elite:
                                break;
                            case BattleType.Boss:
                                break;
                            default: break;
                        }

                        break;
                    case SpotEventType.Event:
                        switch ((EventType) key.index)
                        {
                            case EventType.GetArtifact:
                                eventItem = new GetArtifact(uiGame);
                                break;
                            case EventType.HealingLake:
                                eventItem = new HealingLake(uiGame);
                                break;
                            case EventType.None:
                            default: break;
                        }

                        break;
                    case SpotEventType.None:
                        break;
                    case SpotEventType.Elite:
                        break;
                    case SpotEventType.Boss:
                        break;
                    case SpotEventType.Rest:
                        break;
                    case SpotEventType.Artefact:
                        break;
                    default:
                        break;
                }
                EventBuffer[key] = eventItem;
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