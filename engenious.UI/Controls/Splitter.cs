﻿using engenious.Graphics;
using engenious.Input;

namespace engenious.UI.Controls
{
    /// <summary>
    /// Container zur Aufslittung zweier Controls in zwei größenveränderbare Panels.
    /// </summary>
    public class Splitter : Control
    {
        private Control slot1;

        private int? slot1MinSize;

        private int? slot1MaxSize;

        private Control slot2;

        private int? slot2MinSize;

        private int? slot2MaxSize;

        private int splitterSize;

        private int splitterPosition;

        private readonly PropertyEventArgs<Control> _slot1ChangedEventArgs = new PropertyEventArgs<Control>();
        /// <summary>
        /// Inhalt des ersten Panels (je nach Orientierung oben oder links)
        /// </summary>
        public Control Slot1
        {
            get { return slot1; }
            set
            {
                if (slot1 == value) return;

                _slot1ChangedEventArgs.OldValue = slot1;
                _slot1ChangedEventArgs.NewValue = value;
                _slot1ChangedEventArgs.Handled = false;

                if (slot1 != null)
                {
                    Children.Remove(slot1);
                    slot1 = null;
                }

                if (value != null)
                {
                    slot1 = value;
                    Children.Add(slot1);
                }

                InvalidateDimensions();

                OnSlot1Changed(_slot1ChangedEventArgs);
                Slot1Changed?.Invoke(this, _slot1ChangedEventArgs);
            }
        }
        private readonly PropertyEventArgs<int?> _slot1MinSizeChangedEventArgs = new PropertyEventArgs<int?>();
        /// <summary>
        /// Mindestgröße des ersten Panels oder null, falls egal
        /// </summary>
        public int? Slot1MinSize
        {
            get { return slot1MinSize; }
            set
            {
                if (slot1MinSize == value) return;

                _slot1MinSizeChangedEventArgs.OldValue = slot1MinSize;
                _slot1MinSizeChangedEventArgs.NewValue = value;
                _slot1MinSizeChangedEventArgs.Handled = false;

                slot1MinSize = value;

                InvalidateDimensions();

                OnSlot1MinSizeChanged(_slot1MinSizeChangedEventArgs);
                Slot1MinSizeChanged?.Invoke(this, _slot1MinSizeChangedEventArgs);
            }
        }

        private readonly PropertyEventArgs<int?> _slot1MaxSizeChangedEventArgs = new PropertyEventArgs<int?>();
        /// <summary>
        /// Maximalgröße des ersten Panels oder null, falls egal.
        /// </summary>
        public int? Slot1MaxSize
        {
            get { return slot1MaxSize; }
            set
            {
                if (slot1MaxSize == value) return;
                
                _slot1MaxSizeChangedEventArgs.OldValue = slot1MaxSize;
                _slot1MaxSizeChangedEventArgs.NewValue = value;
                _slot1MaxSizeChangedEventArgs.Handled = false;

                slot1MaxSize = value;

                InvalidateDimensions();

                OnSlot1MaxSizeChanged(_slot1MaxSizeChangedEventArgs);
                Slot1MaxSizeChanged?.Invoke(this, _slot1MaxSizeChangedEventArgs);
            }
        }
        private readonly PropertyEventArgs<Control> _slot2ChangedEventArgs = new PropertyEventArgs<Control>();
        /// <summary>
        /// Inhalt des zweiten Panels (je nach Orientierung unten oder rechts).
        /// </summary>
        public Control Slot2
        {
            get { return slot2; }
            set
            {
                if (slot2 == value) return;

                _slot2ChangedEventArgs.OldValue = slot2;
                _slot2ChangedEventArgs.NewValue = value;
                _slot2ChangedEventArgs.Handled = false;

                if (slot2 != null)
                {
                    Children.Remove(slot2);
                    slot2 = null;
                }

                if (value != null)
                {
                    slot2 = value;
                    Children.Add(slot2);
                }

                InvalidateDimensions();

                OnSlot2Changed(_slot2ChangedEventArgs);
                Slot2Changed?.Invoke(this, _slot2ChangedEventArgs);
            }
        }

        private readonly PropertyEventArgs<int?> _slot2MinSizeChangedEventArgs = new PropertyEventArgs<int?>();
        /// <summary>
        /// Mindestgröße des zweiten Panels oder null, falls egal.
        /// </summary>
        public int? Slot2MinSize
        {
            get { return slot2MinSize; }
            set
            {
                if (slot2MinSize == value) return;

                _slot2MinSizeChangedEventArgs.OldValue = slot2MinSize;
                _slot2MinSizeChangedEventArgs.NewValue = value;
                _slot2MinSizeChangedEventArgs.Handled = false;

                slot2MinSize = value;

                InvalidateDimensions();

                OnSlot2MinSizeChanged(_slot2MinSizeChangedEventArgs);
                Slot2MinSizeChanged?.Invoke(this, _slot2MinSizeChangedEventArgs);
            }
        }
        private readonly PropertyEventArgs<int?> _slot2MaxSizeChangedEventArgs = new PropertyEventArgs<int?>();
        /// <summary>
        /// Maximalgröße des zweiten Panels oder null, falls egal.
        /// </summary>
        public int? Slot2MaxSize
        {
            get { return slot2MaxSize; }
            set
            {
                if (slot2MaxSize == value) return;

                _slot2MaxSizeChangedEventArgs.OldValue = slot2MaxSize;
                _slot2MaxSizeChangedEventArgs.NewValue = value;
                _slot2MaxSizeChangedEventArgs.Handled = false;

                slot2MaxSize = value;

                InvalidateDimensions();

                OnSlot2MaxSizeChanged(_slot2MaxSizeChangedEventArgs);
                Slot2MaxSizeChanged?.Invoke(this, _slot2MaxSizeChangedEventArgs);
            }
        }

        private readonly PropertyEventArgs<int> _splitterSizeChangedEventArgs = new PropertyEventArgs<int>();
        /// <summary>
        /// Die Breite des Splitt-Bereichs.
        /// </summary>
        public int SplitterSize
        {
            get { return splitterSize; }
            set
            {
                if (splitterSize == value) return;

                _splitterSizeChangedEventArgs.OldValue = splitterSize;
                _splitterSizeChangedEventArgs.NewValue = value;
                _splitterSizeChangedEventArgs.Handled = false;

                splitterSize = value;

                InvalidateDimensions();

                OnSplitterSizeChanged(_splitterSizeChangedEventArgs);
                SplitterSizeChanged?.Invoke(this, _splitterSizeChangedEventArgs);
            }
        }

        private readonly PropertyEventArgs<int> _splitterPositionChangedEventArgs = new PropertyEventArgs<int>();
        /// <summary>
        /// Aktuelle Position des Splitters.
        /// </summary>
        public int SplitterPosition
        {
            get { return splitterPosition; }
            set
            {
                if (splitterPosition == value) return;

                _splitterPositionChangedEventArgs.OldValue = splitterPosition;
                _splitterPositionChangedEventArgs.NewValue = value;
                _splitterPositionChangedEventArgs.Handled = false;

                splitterPosition = value;
                InvalidateDimensions();

                OnSplitterPositionChanged(_splitterPositionChangedEventArgs);
                SplitterPositionChanged?.Invoke(this, _splitterPositionChangedEventArgs);
            }
        }

        /// <summary>
        /// Gibt die tatsächliche Splitterposition zurück.
        /// </summary>
        public int ActualSplitterPosition
        {
            get
            {
                int result = SplitterPosition;

                // Größenlimits des ersten Slots
                if (Slot1MinSize.HasValue && result < Slot1MinSize.Value)
                    result = Slot1MinSize.Value;
                if (Slot1MaxSize.HasValue && result > Slot1MaxSize.Value)
                    result = Slot1MaxSize.Value;

                // unterer / rechter Control-Rand
                if (Orientation == Orientation.Horizontal)
                {
                    // SLot2-Limits berücksichtigen
                    int slot2width = ActualClientSize.X - SplitterSize - result;
                    if (Slot2MinSize.HasValue && slot2width < Slot2MinSize.Value)
                        result = ActualClientSize.X - SplitterSize - Slot2MinSize.Value;
                    if (Slot2MaxSize.HasValue && slot2width > Slot2MaxSize.Value)
                        result = ActualClientSize.X - SplitterSize - Slot2MaxSize.Value;

                    if (result >= ActualSize.X - SplitterSize)
                        result = ActualSize.X - SplitterSize;
                }
                else if (Orientation == Orientation.Vertical)
                {
                    // SLot2-Limits berücksichtigen
                    int slot2height = ActualClientSize.Y - SplitterSize - result;
                    if (Slot2MinSize.HasValue && slot2height < Slot2MinSize.Value)
                        result = ActualClientSize.Y - SplitterSize - Slot2MinSize.Value;
                    if (Slot2MaxSize.HasValue && slot2height > Slot2MaxSize.Value)
                        result = ActualClientSize.Y - SplitterSize - Slot2MaxSize.Value;

                    if (result >= ActualSize.Y - SplitterSize)
                        result = ActualSize.Y - SplitterSize;
                }

                // oberer / linker Control-Rand
                if (result < 0) result = 0;

                return result;
            }
        }

        /// <summary>
        /// Gibt die Ausrichtung des Splitters an oder legt diese fest. Horizontal 
        /// splittet den Bereich in Links (Slot1) und Rechts (Slot2), Vertikal 
        /// hingegen in Oben (Slot1) und Unten (Slot2).
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Der Brush der für das Zeichnen des Splitters (bei horizontaler 
        /// Orientierung) verwendet werden soll.
        /// </summary>
        public Brush SplitterBrushHorizontal { get; set; }

        /// <summary>
        /// Der Brush der für das Zeichnen des Splitters (bei vertikaler 
        /// Orientierung) verwendet werden soll.
        /// </summary>
        public Brush SplitterBrushVertical { get; set; }

        public Splitter(BaseScreenComponent manager, string style = "")
            : base(manager, style)
        {
            CanFocus = true;
            TabStop = true;

            ApplySkin(typeof(Splitter));
        }

        public override Point GetExpectedSize(Point available)
        {
            if (!Visible) return Point.Zero;
            return GetMaxClientSize(available) + Borders;
        }

        public override void SetActualSize(Point available)
        {
            if (!Visible)
            {
                SetDimension(Point.Zero, available);
                return;
            }

            Point minSize = GetExpectedSize(available);
            SetDimension(minSize, available);

            Rectangle client = ActualClientArea;
            if (Orientation == Orientation.Horizontal)
            {
                // Trennung zwischen links und rechts
                if (Slot1 != null)
                    Slot1.SetActualSize(new Point(ActualSplitterPosition, client.Height));
                if (Slot2 != null)
                {
                    Slot2.SetActualSize(new Point(client.Width - SplitterSize - ActualSplitterPosition, client.Height));
                    Slot2.ActualPosition += new Point(ActualSplitterPosition + SplitterSize, 0);
                }
            }
            else if (Orientation == Orientation.Vertical)
            {
                // Trennung zwischen oben und unten
                if (Slot1 != null)
                    Slot1.SetActualSize(new Point(client.Width, ActualSplitterPosition));
                if (Slot2 != null)
                {
                    Slot2.SetActualSize(new Point(client.Width, client.Height - SplitterSize - ActualSplitterPosition));
                    Slot2.ActualPosition += new Point(0, ActualSplitterPosition + SplitterSize);
                }
            }
        }

        private bool dragging = false;

        protected override void OnLeftMouseDown(MouseEventArgs args)
        {
            if (Orientation == Orientation.Horizontal)
            {
                if (args.LocalPosition.X > ActualSplitterPosition &&
                    args.LocalPosition.X <= ActualSplitterPosition + SplitterSize)
                {
                    dragging = true;
                    args.Handled = true;
                }
            }
            else if (Orientation == Orientation.Vertical)
            {
                if (args.LocalPosition.Y > ActualSplitterPosition &&
                    args.LocalPosition.Y <= ActualSplitterPosition + SplitterSize)
                {
                    dragging = true;
                    args.Handled = true;
                }
            }
        }

        protected override void OnLeftMouseUp(MouseEventArgs args)
        {
            if (dragging)
            {
                dragging = false;
                args.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            if (dragging)
            {
                if (Orientation == Orientation.Horizontal)
                    SplitterPosition = args.LocalPosition.X;
                else if (Orientation == Orientation.Vertical)
                    SplitterPosition = args.LocalPosition.Y;
                args.Handled = true;
            }
        }

        protected override void OnDrawBackground(SpriteBatch batch, Rectangle backgroundArea, GameTime gameTime, float alpha)
        {
            base.OnDrawBackground(batch, backgroundArea, gameTime, alpha);

            // Splitter Brush anwenden (Horizontal)
            if (Orientation == Orientation.Horizontal && SplitterBrushHorizontal != null)
            {
                Rectangle area = new Rectangle(
                    backgroundArea.X + ActualSplitterPosition, backgroundArea.Y,
                    SplitterSize, backgroundArea.Height);
                SplitterBrushHorizontal.Draw(batch, area, alpha);
            }

            // SPlitter Brush anwenden (Vertical)
            if (Orientation == Orientation.Vertical && SplitterBrushVertical != null)
            {
                Rectangle area = new Rectangle(
                    backgroundArea.X, backgroundArea.Y + ActualSplitterPosition,
                    backgroundArea.Width, SplitterSize);
                SplitterBrushVertical.Draw(batch, area, alpha);
            }
        }

        protected override void OnDrawFocusFrame(SpriteBatch batch, Rectangle contentArea, GameTime gameTime, float alpha)
        {
            if (Skin.Current.FocusFrameBrush != null)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    Rectangle area = new Rectangle(
                        contentArea.X + ActualSplitterPosition, contentArea.Y,
                        SplitterSize, contentArea.Height);
                    Skin.Current.FocusFrameBrush.Draw(batch, area, alpha);
                }

                if (Orientation == Orientation.Vertical)
                {
                    Rectangle area = new Rectangle(
                        contentArea.X, contentArea.Y + ActualSplitterPosition,
                        contentArea.Width, SplitterSize);
                    Skin.Current.FocusFrameBrush.Draw(batch, area, alpha);
                }
            }
        }

        protected override void OnKeyPress(KeyEventArgs args)
        {
            if (Focused == TreeState.Active)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    if (args.Key == Keys.Left)
                    {
                        SplitterPosition = ActualSplitterPosition - 5;
                        args.Handled = true;
                    }
                    if (args.Key == Keys.Right)
                    {
                        SplitterPosition = ActualSplitterPosition + 5;
                        args.Handled = true;
                    }
                }
                if (Orientation == Orientation.Vertical)
                {
                    if (args.Key == Keys.Up)
                    {
                        SplitterPosition = ActualSplitterPosition - 5;
                        args.Handled = true;
                    }
                    if (args.Key == Keys.Down)
                    {
                        SplitterPosition = ActualSplitterPosition + 5;
                        args.Handled = true;
                    }
                }
            }
            base.OnKeyPress(args);
        }

        protected virtual void OnSlot1Changed(PropertyEventArgs<Control> args) { }

        protected virtual void OnSlot1MinSizeChanged(PropertyEventArgs<int?> args) { }

        protected virtual void OnSlot1MaxSizeChanged(PropertyEventArgs<int?> args) { }

        protected virtual void OnSlot2Changed(PropertyEventArgs<Control> args) { }

        protected virtual void OnSlot2MinSizeChanged(PropertyEventArgs<int?> args) { }

        protected virtual void OnSlot2MaxSizeChanged(PropertyEventArgs<int?> args) { }

        protected virtual void OnSplitterPositionChanged(PropertyEventArgs<int> args) { }

        protected virtual void OnSplitterSizeChanged(PropertyEventArgs<int> args) { }

        /// <summary>
        /// Event signalisiert eine Änderung des Controls in Slot 1.
        /// </summary>
        public event PropertyChangedDelegate<Control> Slot1Changed;

        /// <summary>
        /// Event signalisiert eine Änderung des Controls in Slot 2.
        /// </summary>
        public event PropertyChangedDelegate<Control> Slot2Changed;

        /// <summary>
        /// Event signalisiert eine Änderung bei der minimalen Größe von Slot 1.
        /// </summary>
        public event PropertyChangedDelegate<int?> Slot1MinSizeChanged;

        /// <summary>
        /// Event signalisiert eine Änderung bei der maximalen Größe von Slot 1.
        /// </summary>
        public event PropertyChangedDelegate<int?> Slot1MaxSizeChanged;

        /// <summary>
        /// Event signalisiert eine Änderung bei der minimalen Größe von Slot 2.
        /// </summary>
        public event PropertyChangedDelegate<int?> Slot2MinSizeChanged;

        /// <summary>
        /// Event signalisiert eine Änderung bei der maximalen Größe von Slot 2.
        /// </summary>
        public event PropertyChangedDelegate<int?> Slot2MaxSizeChanged;

        /// <summary>
        /// Event signalisiert eine Änderung der aktuellen Splitter-Position.
        /// </summary>
        public event PropertyChangedDelegate<int> SplitterPositionChanged;

        /// <summary>
        /// Event signalisiert eine Änderung der Splitter-Größe.
        /// </summary>
        public event PropertyChangedDelegate<int> SplitterSizeChanged;
    }
}
