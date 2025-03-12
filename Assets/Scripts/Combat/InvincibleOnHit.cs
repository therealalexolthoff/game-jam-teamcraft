using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

public class InvincibleOnHit : MonoBehaviour
{
    [FormerlySerializedAs("invincibilityLength")] public float invincibilityDuration = 3f;

    public Color colorOnHit;

    // Update is called once per frame
    public void InvincibleStart()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color normalColor = spriteRenderer.color;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        
        // PrimeTween example. Learn more about PrimeTween here: https://github.com/KyryloKuzyk/PrimeTween
        
        // A blinking red effect. By the way, I'm specifying the variable names here, but it's optional.
        // It's regular C# syntax that can be used in any function, but I thought it would be especially helpful here to show what each variable is.
        Tween colorTween = Tween.Color(target: GetComponent<SpriteRenderer>(), // We are creating a new Color tween that affects our SpriteRenderer.
            endValue: colorOnHit, // This is the color that the tween is moving towards.
            duration: 0.2f, // This is the duration of the tween. Since this tween loops, this is the duration of each iteration
            ease: Ease.InOutSine, // What easing function to use to interprolate between the start and end values. More info here: http://easings.net/
            cycles: -1, // How many times this tween loops before ending. I have this set to -1 since I'll be ending it with a script.
            cycleMode: CycleMode.Yoyo); // How the tween should handle multiple cycles. Yo-yo means it will alternate between transitioning from start to end, like a yo-yo going up and down.
        
        // This is a special type of tween that doesn't animate anything, it just calls a function after so many seconds.
        Tween.Delay(target: this, // The target is optional for delays. If I assign a target, PrimeTween will automatically kill this tween if the target is destroyed before the tween finishes. It's good practice to always have a target.
            duration: invincibilityDuration, // The delay's duration. No surprises here.
            onComplete: () => // The function to call when the delay finishes. Here, I'm using a shorthand lambda function.
            {
                colorTween.Stop();
                spriteRenderer.color = normalColor;
                collider.enabled = true;
            });
    }
}
