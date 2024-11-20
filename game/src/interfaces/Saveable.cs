using Godot.Collections;

public interface Saveable {
    abstract void ImportData(Dictionary data);
    abstract Dictionary ExportData();
    abstract bool GetIsSaved();
    abstract void SetIsSaved(bool saved);
}