

namespace System.Windows.Forms
{
    public class DropShadowConfigration
    {
        private DropShadowForm dropShadowForm;

        public int ShadowSpread { 
            get => dropShadowForm.ShadowSpread;
            set=> dropShadowForm.ShadowSpread = value;
        }
        
        public int ShadowBlur
        {
            get => dropShadowForm.ShadowBlur;
            set => dropShadowForm.ShadowBlur = value;
        }
        
        public int CornerRound
        {
            get => dropShadowForm.CornerRound;
            set => dropShadowForm.CornerRound = value;
        }
        
        public int BorderWidth
        {
            get => dropShadowForm.BorderWidth;
            set => dropShadowForm.BorderWidth = value;
        }
        
        public bool MasterMoveable
        {
            get => dropShadowForm.MasterMoveable;
            set => dropShadowForm.MasterMoveable = value;
        }

        public bool StandardMaximizedBounds
        {
            get => dropShadowForm.StandardMaximizedBounds;
            set => dropShadowForm.StandardMaximizedBounds = value;
        }

        //public FormBorderStyle MasterFormBorderStyle {
        //    get => dropShadowForm.MasterFormBorderStyle;
        //    set => dropShadowForm.MasterFormBorderStyle = value;
        //}

        public DropShadowConfigration(DropShadowForm dropShadowForm)
        {
            this.dropShadowForm = dropShadowForm;
        }


        

        
    }
}
