using Godot;

public partial class Autoloads : Node {
    public static GlobalSignals GlobalSignals(Node node) {
        return (GlobalSignals) node.GetNode("/root/GlobalSignals");
    }

    public static EngineLifecycleHandler EngineLifecycleHandler(Node node) {
        return (EngineLifecycleHandler) node.GetNode("/root/EngineLifecycleHandler");
    }

    public static Functions Functions(Node node) {
        return (Functions) node.GetNode("/root/Functions");
    }
 
}