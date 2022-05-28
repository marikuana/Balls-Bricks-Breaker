using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseBlockFactory : ScriptableObject
{
    [SerializeField]
    protected Block pref;

    public abstract Block Get(BlockType blockType);
}
