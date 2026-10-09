using UnityEngine;
using UnityEngine.UI;

namespace FlightSim.Platform.Presentation
{
    [RequireComponent(typeof(CanvasRenderer))]
    public sealed class HudVectorGraphic : MaskableGraphic
    {
        private HudVectorCommandBuffer _commands;

        public int CommandCount => _commands != null ? _commands.Count : 0;

        public void SetCommands(HudVectorCommandBuffer commands)
        {
            _commands = commands;
            SetVerticesDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vertexHelper)
        {
            vertexHelper.Clear();
            if (_commands == null)
                return;

            for (int i = 0; i < _commands.Count; i++)
                AddLineQuad(vertexHelper, _commands.GetLine(i));
        }

        private static void AddLineQuad(VertexHelper vertexHelper, HudLineCommand command)
        {
            Vector2 delta = command.End - command.Start;
            float length = delta.magnitude;
            if (length < 0.001f)
                return;

            Vector2 normal = new Vector2(-delta.y, delta.x) / length * (command.Width * 0.5f);
            int firstVertex = vertexHelper.currentVertCount;

            AddVertex(vertexHelper, command.Start - normal, command.Color);
            AddVertex(vertexHelper, command.Start + normal, command.Color);
            AddVertex(vertexHelper, command.End + normal, command.Color);
            AddVertex(vertexHelper, command.End - normal, command.Color);

            vertexHelper.AddTriangle(firstVertex, firstVertex + 1, firstVertex + 2);
            vertexHelper.AddTriangle(firstVertex, firstVertex + 2, firstVertex + 3);
        }

        private static void AddVertex(VertexHelper vertexHelper, Vector2 position, Color32 color)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.position = position;
            vertex.color = color;
            vertex.uv0 = Vector2.zero;
            vertexHelper.AddVert(vertex);
        }
    }
}
