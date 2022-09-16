using CodeWalker;
using CodeWalker.GameFiles;
using SharpDX;

namespace Arbolito
{
    public class PropReplacer
    {
        public MetaHash FromProp => JenkHash.GenHash(FromPropStr);
        public string FromPropStr { get; set; }
        public MetaHash ToProp => JenkHash.GenHash(ToPropStr);
        public string ToPropStr { get; set; }
        
        public Vector3 RotationOffset { get; set; }

        public bool ChangeRotation => !RotationOffset.IsZero;


    }
}
