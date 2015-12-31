using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace UEL {
  public abstract class UELTexture {
    public abstract Color color { get; set; }
    public abstract T GetComponent<T>();
  }

  public class UELImage : UELTexture {
    Image img;
    public UELImage(Image img) { this.img = img; }
    public override Color color { get { return img.color; } set { img.color = value; } }

    public override T GetComponent<T>() {
      return img.GetComponent<T>();
    }
  }

  public class UELSpriteRenderer : UELTexture {
    SpriteRenderer spr;
    public UELSpriteRenderer(SpriteRenderer spr) { this.spr = spr; }
    public override Color color { get { return spr.color; } set { spr.color = value; } }

    public override T GetComponent<T>() {
      return spr.GetComponent<T>();
    }
  }
}
