﻿using engenious.Graphics;

namespace engenious.UI.Controls
{
    interface ITextControl: IControl
    {
        string Text { get; set; }

        SpriteFont Font { get; set; }

        Color TextColor { get; set; }

        HorizontalAlignment HorizontalTextAlignment { get; set; }

        VerticalAlignment VerticalTextAlignment { get; set; }

        bool WordWrap { get; set; }
        bool LineWrap { get; set; }
    }
}
