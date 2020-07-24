using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
  public delegate void PanelDbRenderEventHandler(object sender, PanelDbRenderEventArgs e);

  public class PanelDbRenderEventArgs : EventArgs
  {
    public Graphics Graphics { get; private set; }

    public PanelDbRenderEventArgs(Graphics graphics)
    {
      Graphics = graphics;
    }
  }

  public class PanelDb : Panel
  {
    private bool isRunning = false;

    public event PanelDbRenderEventHandler RenderFrame;
    public event EventHandler UpdateFrame;

    public PanelDb()
    {
      DoubleBuffered = true;
    }

    public void Start()
    {
      if (isRunning)
      {
        return;
      }

      isRunning = true;
      GameLoop();
    }

    private async void GameLoop()
    {
      while (isRunning)
      {
        await Task.Yield();
        await Task.Delay(1);
        FireUpdateFrame();
        Invalidate();
      }
    }

    private void FireUpdateFrame()
    {
      var update = UpdateFrame;
      if (update != null)
      {
        update(this, EventArgs.Empty);
      }
    }

    private void FireRenderFrame(Graphics graphics)
    {
      var render = RenderFrame;
      if (render != null)
      {
        render(this, new PanelDbRenderEventArgs(graphics));
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      FireRenderFrame(e.Graphics);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        isRunning = false;
      }
      base.Dispose(disposing);
    }
  }
}
