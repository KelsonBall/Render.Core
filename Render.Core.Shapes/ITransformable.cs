﻿using Render.Core.Transforms;

namespace KelsonBall.Geometry
{
    interface ITransformable<TType, TVector>
    {
        TType Transform(Transform<TVector> transform);
    }
}
