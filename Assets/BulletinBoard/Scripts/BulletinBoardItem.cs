using UnityEngine;

namespace Flexalon.Examples
{
    [RequireComponent(typeof(FlexalonInteractable), typeof(FlexalonConstraint))]
    public class BulletinBoardItem : MonoBehaviour
    {
        public uint CellsWide;
        public uint CellsTall;
    }
}