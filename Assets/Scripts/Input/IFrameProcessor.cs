using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs {
    public interface IFrameProcessor
    {
        public void ProcessInputFrame(InputFrame iframe);
        public LoadableUnit GetLoadableUnit();
    }
}