using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CellAnimation : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI points;

    private float moveTime = .1f;
    private float apeearTime = .1f;

    private Sequence sequence;

   public void Move(Cell from, Cell to, bool IsMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);

        image.color = ColourManager.Instance.CellColors[from.Value];
        points.text = from.Points.ToString();
        points.color = from.Value <= 2 ?
            ColourManager.Instance.PointsDarkColor :
            ColourManager.Instance.PointsLightColor;

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if (IsMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.color = ColourManager.Instance.CellColors[to.Value];
                points.text = to.Points.ToString();
                points.color = to.Value <= 2 ?
                    ColourManager.Instance.PointsDarkColor :
                    ColourManager.Instance.PointsLightColor;
            });

            sequence.Append(transform.DOScale(1.2f, apeearTime));
            sequence.Append(transform.DOScale(1f, apeearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        image.color = ColourManager.Instance.CellColors[cell.Value];
        points.text = cell.Points.ToString();
        points.color = cell.Value <= 2 ?
                    ColourManager.Instance.PointsDarkColor :
                    ColourManager.Instance.PointsLightColor;

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, apeearTime * 2));
        sequence.Append(transform.DOScale(1f, apeearTime * 2));
        sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });

    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
