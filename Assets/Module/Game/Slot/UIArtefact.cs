using Infra.Model.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Slot
{
    internal class UIArtefact : BaseMonoBehaviour
    {
        private Artefact Artefact { get; set; }

        public void Set(Artefact artefact)
        {
            Artefact = artefact;

            GetComponentInChildren<Text>().text = Artefact.Name;

        }
    }
}
