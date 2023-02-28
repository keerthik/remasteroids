using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs {
    public class ReplayData
    {
        [SerializeField] private Queue<InputFrame> data;
        private bool isLoaded = false;
        public bool HasLoaded => isLoaded;

        public string replayFileName = null;
        public bool HasMoreFrames => (data?.Count ?? 0) > 0;

        public string filename;
        public void RecordInputForPlayer1(InputFrame frame) {
            if (frame.HasNoGameInput) return;
            data.Enqueue(frame);
        }

        public void Prepare() {
            data = new();
            Debug.Log("Replay class prepared");
        }

        public void LoadData(string name = null) {
            if (isLoaded) return;
            data = new();
            // Load data from file here
            // This should be async eventually
            using FileStream stream = File.Open(ReplayFile(name), FileMode.Open);
            using BinaryReader reader = new(stream, System.Text.Encoding.UTF8, false);
            int count = 0;
            while (stream.Position < stream.Length && ++count < 5000) {
                string framedata = reader.ReadString();
                InputFrame frame = InputFrame.FromString(framedata);
                RecordInputForPlayer1(frame);
            }
            Debug.Log($"Replay loaded with {data.Count} frames");
            isLoaded = true;
        }

        private string ReplayFile(string name = null) {
            return Path.Join(Application.persistentDataPath, $"{name ?? $"LatestSave_{System.DateTime.UtcNow:yyyyMMdd_HHmm}"}.asr");
        }

        public void SaveDataToDisk(string name = null) {
            // save data to file
            filename = ReplayFile(name);

            using FileStream stream = File.Open(filename, FileMode.OpenOrCreate);
            using BinaryWriter writer = new(stream, System.Text.Encoding.UTF8, false);
            
            while (HasMoreFrames) {
                string towrite = GetNextInputFrame().ToString();
                Debug.Log(towrite);
                writer.Write(towrite);
            }
            Debug.Log($"Saved data to ${filename}");

        }

        public InputFrame GetNextInputFrame() {
            if (data.Count != 0) return data.Dequeue();
            else return null;
        }

        public void Reset() {}
    }
}