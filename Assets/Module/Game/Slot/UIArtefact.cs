using Infra.Model.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Slot
{
    internal class UIArtefact : MonoBehaviour
    {
        private Artefact Artefact { get; set; }

        public void Set(Artefact artefact)
        {
            Artefact = artefact;
        }
    }
}
