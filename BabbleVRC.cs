using System.Reflection;
using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.Babble;
public class BabbleVRC : ExtTrackingModule
{
    private BabbleOSC babbleOSC;
    private UnifiedExpressions[] unifiedExpressions;

    public override (bool SupportsEye, bool SupportsExpression) Supported => (false, true);

    public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
    {
        unifiedExpressions = Enum.GetValues<UnifiedExpressions>();
        babbleOSC = new BabbleOSC(Logger);

        List<Stream> streams = new List<Stream>();
        Assembly a = Assembly.GetExecutingAssembly();
        var hmdStream = a.GetManifestResourceStream
            ("VRCFaceTracking.Babble.BabbleLogo.png");
        streams.Add(hmdStream);
        ModuleInformation = new ModuleMetadata()
        {
            Name = "Project Babble Face Tracking\nInference Model v2.0.0"
        };

        ModuleInformation.StaticImages = streams;
        return (false, true);
    }

    public override void Teardown() => babbleOSC.Teardown();
    public override void Update()
    {
        foreach (var expression in unifiedExpressions)
        {
            UnifiedTracking.Data.Shapes[(int)expression].Weight = babbleOSC.BabbleExpressionMap.GetByKey1(expression);
        }
    }
}
