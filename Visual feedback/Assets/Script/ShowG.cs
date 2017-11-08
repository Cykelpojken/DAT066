using UnityEngine;

public class ShowG : MonoBehaviour {
    SpriteRenderer rend;
    Sprite[] sprites;
    float Xl, Yl;
    private void Start()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }

    public void Click()
    {
        Draw(true, 10, 20, 10, 20);
    }

    public void Draw(bool correct, float X1, float X2, float Y1, float Y2) {
        Vector3 middle = CalculateScaleAndPosition(X1, X2, Y1, Y2);
        if (correct) {
            rend.sprite = sprites[0];
            rend.enabled = true;
            transform.position = middle;
            transform.localScale = new Vector3(Xl / 1000, Yl / 1000, 0);
        }
        else
        {
            rend.sprite = sprites[1];
            rend.enabled = true;
            transform.position = middle;
        }
    }

    private Vector3 CalculateScaleAndPosition(float X1, float X2, float Y1, float Y2) {
        float minY;
        float minX;
        if (X2 > X1)
        {
            Xl = X2 - X1;
            minX = X1;
        }
        else
        {
            Xl = X1 - X2;
            minX = X2;
        }
        if (Y2 > Y1)
        {
            Yl = Y2 - Y1;
            minY = Y1;
        }
        else
        {
            Yl = Y1 - Y2;
            minY = Y2;
        }
        Vector3 middle = new Vector3(Xl/2 + minX, Yl/2 + minY, 0);
        //Vector3 scale = new Vector3(Xl / 1000, Yl / 1000, 0);
        //Vector3[] middleandscale = new[] { new Vector3(Xl / 2 + minX, Yl / 2 + minY, 0), new Vector3(Xl / 1000, Yl / 1000, 0) }; //[0] is middle, [1] is scale
        return middle;
       }
}
