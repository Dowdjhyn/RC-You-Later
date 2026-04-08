using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class StepsData
{
    public long[] steps;
}

public static class Timer
{
    private static readonly Stopwatch stopwatch = new();
    private static List<long> steps = new();

    public static bool IsRunning
    {
        get => stopwatch.IsRunning;
    }

    public static double ElapsedSeconds
    {
        get => stopwatch.ElapsedMilliseconds * 0.001f;
    }

    public static int StepsCount
    {
        get => steps.Count;
    }

    public static double GetStepElapsedSeconds(int index)
    {
        return steps[index] * 0.001f;
    }

    public static void Reset()
    {
        stopwatch.Reset();
        steps.Clear();
    }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static void Stop()
    {
        stopwatch.Stop();
    }

    public static void Step()
    {
        steps.Add(stopwatch.ElapsedMilliseconds);
    }

    public static void Save()
    {
        string filePath = Application.persistentDataPath + "/score.json";

        // Vérifier s'il existe déjà un fichier
        if (System.IO.File.Exists(filePath))
        {
            // Lire l'ancien score
            string oldJson = System.IO.File.ReadAllText(filePath);
            StepsData oldData = JsonUtility.FromJson<StepsData>(oldJson);

            long oldTime = oldData.steps[oldData.steps.Length - 1];
            long newTime = stopwatch.ElapsedMilliseconds;

            // Si le nouveau temps n'est pas meilleur, ne pas sauvegarder
            if (newTime > oldTime)
            {
                UnityEngine.Debug.Log("Pas assez rapide. Ancien: " + oldTime + "ms, Nouveau: " + newTime + "ms");
                return;
            }
        }

        // Sauvegarder le nouveau score
        StepsData data = new StepsData();
        data.steps = steps.ToArray();

        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath, json);

        UnityEngine.Debug.Log("Score sauvegarde!");
    }

    public static void Load()
    {
        string filePath = Application.persistentDataPath + "/score.json";

        if (!System.IO.File.Exists(filePath))
        {
            UnityEngine.Debug.Log("Aucun fichier trouve");
            return;
        }

        string json = System.IO.File.ReadAllText(filePath);
        StepsData data = JsonUtility.FromJson<StepsData>(json);

        steps = new List<long>(data.steps);

        UnityEngine.Debug.Log("Score charge!");
    }
}

 