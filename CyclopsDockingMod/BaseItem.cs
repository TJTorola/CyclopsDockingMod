using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace CyclopsDockingMod
{
    public abstract class BaseItem : Nautilus.Assets.CustomPrefab, IBaseItem
    {
        [SetsRequiredMembers]
        public BaseItem(string classID, string name, string desc, string icon) : this(PrefabInfo.WithTechType(classID, name, desc, unlockAtStart: true).WithFileName(DefaultResourcePath + classID).WithIcon(AssetsHelper.Assets.LoadAsset<Sprite>(icon)))
        {
        }

        [SetsRequiredMembers]
        protected BaseItem(PrefabInfo info) : base(info)
        {
            SetGameObject(GetGameObject);
        }

        public const string DefaultResourcePath = "WorldEntities/Environment/Wrecks/";

        public bool IsRegistered = false;

        public bool IsHabitatBuilder = false;

        public GameObject GameObject { get; set; }

        public RecipeData Recipe { get; set; }
        public string ClassID => Info.ClassID;
        public string PrefabFileName => Info.PrefabFileName;
        public TechType TechType => Info.TechType;
        public abstract GameObject GetGameObject();

        public virtual void RegisterItem()
        {
            if (this.IsRegistered == false && this.GameObject != null)
            {
                if (this.Recipe != null)
                    CraftDataHandler.SetRecipeData(this.Info.TechType, this.Recipe);

                this.Register();

                this.IsRegistered = true;
            }
        }
    }
}
