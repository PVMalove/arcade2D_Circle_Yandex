using System;
using System.Collections.Generic;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins.BodySkin;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins.FaceSkin;

namespace CodeBase.UI.Popups.SkinsShop.TEST.Data
{
    public class SkinData
    {
        public IEnumerable<BodySkins> OpenBodySkins => openedBodySkins;
        public IEnumerable<FaceSkins> OpenFaceSkins => openedFaceSkins;
        
        private BodySkins selectedBodySkins;
        private FaceSkins selectedFaceSkins;
        
        private List<BodySkins> openedBodySkins;
        private List<FaceSkins> openedFaceSkins;

        public SkinData()
        {
            selectedBodySkins = BodySkins.blue_body_circle;
            selectedFaceSkins = FaceSkins.face_a;
            openedBodySkins = new List<BodySkins>(){selectedBodySkins};
            openedFaceSkins = new List<FaceSkins>(){selectedFaceSkins};
        }

        public BodySkins SelectedBodySkins
        {
            get => selectedBodySkins;
            set
            {
                if (openedBodySkins.Contains(value) == false)
                    throw new ArgumentException(nameof(value));
                selectedBodySkins = value;
            }
        }
        
        public FaceSkins SelectedFaceSkins
        {
            get => selectedFaceSkins;  
            set
            {
                if (openedFaceSkins.Contains(value) == false)
                    throw new ArgumentException(nameof(value));
                selectedFaceSkins = value;
            }
        }

        public void OpenBodySkin(BodySkins skin)
        {
            if(openedBodySkins.Contains(skin))
                throw new ArgumentException(nameof(skin));

            openedBodySkins.Add(skin);
        }

        public void OpenFaceSkin(FaceSkins skin)
        {
            if (openedFaceSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));

            openedFaceSkins.Add(skin);
        }
    }
}