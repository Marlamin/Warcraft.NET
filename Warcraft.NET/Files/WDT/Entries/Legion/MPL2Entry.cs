using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.WDT.Entries.Legion
{
    /// <summary>
    /// An entry struct containing point light information (v2)
    /// </summary>
    public class MPL2Entry
    {
        /// <summary>
        /// Light Id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public RGBA Color { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// @deprecated Use LightRadius instead.
        /// </summary>
        public float LightRadius { get { return AttenuationStart; } set { AttenuationStart = value; }}

        /// <summary>
        /// @deprecated Use BlendRadius instead.
        /// </summary>
        public float BlendRadius { get { return AttenuationEnd; } set { AttenuationEnd = value; } }

        /// <summary>
        /// Attenuation Start
        /// </summary>
        public float AttenuationStart { get; set; }

        /// <summary>
        /// Attenuation End
        /// </summary>
        public float AttenuationEnd { get; set; }

        /// <summary>
        /// Intensity
        /// </summary>
        public float Intensity { get; set; }

        /// <summary>
        /// Rotation, only used to rotate lightcookies on point lights.
        /// </summary>
        public Vector3 Rotation { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileX { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileY { get; set; }

        /// <summary>
        /// Index to MLTA chunk entry
        /// Defaults to -1 if not used
        /// </summary>
        public short MLTAIndex { get; set; } = -1;

        /// <summary>
        /// Index to MTEX chunk entry containing a lightcookie texture FDID (note: not related to old ADT MTEX)
        /// Default to -1 if not used
        /// </summary>
        public short MTEXIndex { get; set; } = -1;

        public MPL2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MPL2Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Id = br.ReadUInt32();
                    Color = br.ReadBGRA();
                    Position = br.ReadVector3(AxisConfiguration.Native);
                    AttenuationStart = br.ReadSingle();
                    AttenuationEnd = br.ReadSingle();
                    Intensity = br.ReadSingle();
                    Rotation = br.ReadVector3();
                    TileX = br.ReadUInt16();
                    TileY = br.ReadUInt16();
                    MLTAIndex = br.ReadInt16();
                    MTEXIndex = br.ReadInt16();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 52;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Id);
                bw.WriteBGRA(Color);
                bw.WriteVector3(Position, AxisConfiguration.Native);
                bw.Write(AttenuationStart);
                bw.Write(AttenuationEnd);
                bw.Write(Intensity);
                bw.WriteVector3(Rotation);
                bw.Write(TileX);
                bw.Write(TileY);
                bw.Write(MLTAIndex);
                bw.Write(MTEXIndex);

                return ms.ToArray();
            }
        }
    }
}
