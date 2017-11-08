using UnityEngine;

public class ShowG : MonoBehaviour {
    SpriteRenderer rend;
    Sprite[] sprites;
    float Yl;
    float Xl;
    private GameObject Green;
    private GameObject Red;
    private void Start()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites");

    }

    public void Draw(GameObject sprite, float X1, float X2, float Y1, float Y2) {
       //Vector3 middle = CalculateScale(X1, X2, Y1, Y2);
        sprite.transform.position = CalculateScale(X1, X2, Y1, Y2);
        sprite.transform.localScale = getScale();

        /* rend.sprite = sprites[0];
         rend.enabled = true;
         //transform.position = middle;
         rend.transform.position = middle;
         rend.transform.localScale = new Vector3(Xl / 1000, Yl / 1000, 0);

         rend.sprite = sprites[1];
         rend.enabled = true;
         transform.position = middle;*/

    }

    private Vector3 CalculateScale(float X1, float X2, float Y1, float Y2) {
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

        Vector3 middle = new Vector3(Xl/2 + minX - 320 , Yl/2 - minY + 150, 25000/ Xl );
        return middle;
       }
    private Vector3 getScale()
    {
         return new Vector3(Xl / 1000, Yl / 1000, 0);
    }

}
