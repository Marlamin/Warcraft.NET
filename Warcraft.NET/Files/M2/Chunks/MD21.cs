using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;
using Warcraft.NET.Files.M2.Flags;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.M2.Chunks
{
    /// <summary>
    /// MD21 Chunk - Contains model base information
    /// </summary>
    [AutoDocChunk(0, AutoDocChunkVersionHelper.VersionBeforeLegion, AutoDocChunkVersionHelper.VersionAfterWoD)]
    public class MD21 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MD21";

        public uint Version { get; set; }
        public string Name { get; set; }
        public MD21Flags Flags { get; set; }
        public uint ViewCount { get; set; }
        public BoundingBox VertexBox { get; set; }
        public float VertexBoxRadius { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public float BoundingBoxRadius { get; set; }
        public List<SequenceStruct> Sequences { get; set; }
        public List<AnimationStruct> Animations { get; set; }
        public List<AnimationLookupStruct> AnimationLookups { get; set; }
        public List<BoneStruct> Bones { get; set; }
        public List<KeyBoneLookupStruct> KeyBoneLookup { get; set; }
        public List<VerticeStruct> Vertices { get; set; }
        public List<ColorStruct> Colors { get; set; }
        public List<TextureStruct> Textures { get; set; }
        public List<TransparencyStruct> Transparency { get; set; }
        public List<UVAnimationStruct> UVAnimations { get; set; }
        public List<TextureReplaceStruct> TextureReplace { get; set; }
        public List<RenderFlagStruct> RenderFlags { get; set; }
        public List<BoneLookupTableStruct> BoneLookupTable { get; set; }
        public List<TextureLookupStruct> TextureLookup { get; set; }
        public List<TransparencyLookupStruct> TransparencyLookup { get; set; }
        public List<UVAnimLookupStruct> UVAnimLookup { get; set; }
        public List<BoundingTriangleStruct> BoundingTriangles { get; set; }
        public List<BoundingVertexStruct> BoundingVertices { get; set; }
        public List<BoundingNormalStruct> BoundingNormals { get; set; }
        public List<AttachmentStruct> Attachments { get; set; }
        public List<AttachLookupStruct> AttachLookup { get; set; }
        public List<EventStruct> Events { get; set; }
        public List<LightStruct> Lights { get; set; }
        public List<CameraStruct> Cameras { get; set; }
        public List<CameraLookupStruct> CameraLookup { get; set; }
        public List<RibbonEmitterStruct> RibbonEmitters { get; set; }
        public List<ParticleEmitterStruct> ParticleEmitters { get; set; }


        [Obsolete("Use TextureLookup instead.")]
        public List<TextureLookupStruct> TextrueLookup { get { return TextureLookup; } set { TextureLookup = value; } }

        private byte[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MD21"/> class.
        /// </summary>
        public MD21()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MD21"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MD21(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            data = inData;
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                if (br.ReadBinarySignature(false) != "MD20")
                    throw new Exception("Wrong M2 Header");

                Version = br.ReadUInt32();

                var lenModelname = br.ReadUInt32();
                var ofsModelname = br.ReadUInt32();
                Flags = (MD21Flags)br.ReadUInt32();
                var nSequences = br.ReadUInt32();
                var ofsSequences = br.ReadUInt32();
                var nAnimations = br.ReadUInt32();
                var ofsAnimations = br.ReadUInt32();
                var nAnimationLookup = br.ReadUInt32();
                var ofsAnimationLookup = br.ReadUInt32();
                var nBones = br.ReadUInt32();
                var ofsBones = br.ReadUInt32();
                var nKeyboneLookup = br.ReadUInt32();
                var ofsKeyboneLookup = br.ReadUInt32();
                var nVertices = br.ReadUInt32();
                var ofsVertices = br.ReadUInt32();
                ViewCount = br.ReadUInt32();
                var nColors = br.ReadUInt32();
                var ofsColors = br.ReadUInt32();
                var nTextures = br.ReadUInt32();
                var ofsTextures = br.ReadUInt32();
                var nTransparency = br.ReadUInt32();
                var ofsTransparency = br.ReadUInt32();
                var nUVAnimation = br.ReadUInt32();
                var ofsUVAnimation = br.ReadUInt32();
                var nTexReplace = br.ReadUInt32();
                var ofsTexReplace = br.ReadUInt32();
                var nRenderFlags = br.ReadUInt32();
                var ofsRenderFlags = br.ReadUInt32();
                var nBoneLookupTable = br.ReadUInt32();
                var ofsBoneLookupTable = br.ReadUInt32();
                var nTexLookup = br.ReadUInt32();
                var ofsTexLookup = br.ReadUInt32();
                var nUnk1 = br.ReadUInt32();
                var ofsUnk1 = br.ReadUInt32();
                var nTransLookup = br.ReadUInt32();
                var ofsTranslookup = br.ReadUInt32();
                var nUVAnimLookup = br.ReadUInt32();
                var ofsUVAnimLookup = br.ReadUInt32();
                BoundingBox = br.ReadBoundingBox(AxisConfiguration.Native);
                BoundingBoxRadius = br.ReadSingle();
                VertexBox = br.ReadBoundingBox(AxisConfiguration.Native);
                VertexBoxRadius = br.ReadSingle();
                var nBoundingTriangles = br.ReadUInt32();
                var ofsBoundingTriangles = br.ReadUInt32();
                var nBoundingVertices = br.ReadUInt32();
                var ofsBoundingVertices = br.ReadUInt32();
                var nBoundingNormals = br.ReadUInt32();
                var ofsBoundingNormals = br.ReadUInt32();
                var nAttachments = br.ReadUInt32();
                var ofsAttachments = br.ReadUInt32();
                var nAttachLookup = br.ReadUInt32();
                var ofsAttachLookup = br.ReadUInt32();
                var nEvents = br.ReadUInt32();
                var ofsEvents = br.ReadUInt32();
                var nLights = br.ReadUInt32();
                var ofsLights = br.ReadUInt32();
                var nCameras = br.ReadUInt32();
                var ofsCameras = br.ReadUInt32();
                var nCameraLookup = br.ReadUInt32();
                var ofsCameraLookup = br.ReadUInt32();
                var nRibbonEmitters = br.ReadUInt32();
                var ofsRibbonEmitters = br.ReadUInt32();
                var nParticleEmitters = br.ReadUInt32();
                var ofsParticleEmitters = br.ReadUInt32();

                // Model with flag 8 have extra field
                if (Flags.HasFlag(MD21Flags.UseTextureCombinerCombos))
                {
                    var nUnk2 = br.ReadUInt32();
                    var ofsUnk2 = br.ReadUInt32();
                }

                br.BaseStream.Position = ofsModelname;
                if (lenModelname > 0)
                {
                    Name = new string(br.ReadChars((int)lenModelname));
                    Name = Name.Remove(Name.Length - 1);
                }
                Sequences = ReadStructList<SequenceStruct>(nSequences, ofsSequences, br);
                Animations = ReadStructList<AnimationStruct>(nAnimations, ofsAnimations, br);
                AnimationLookups = ReadStructList<AnimationLookupStruct>(nAnimationLookup, ofsAnimationLookup, br);
                Bones = ReadStructList<BoneStruct>(nBones, ofsBones, br);
                KeyBoneLookup = ReadStructList<KeyBoneLookupStruct>(nKeyboneLookup, ofsKeyboneLookup, br);
                Vertices = ReadStructList<VerticeStruct>(nVertices, ofsVertices, br);
                Colors = ReadStructList<ColorStruct>(nColors, ofsColors, br);
                Textures = ReadTextures(nTextures, ofsTextures, br);
                Transparency = ReadStructList<TransparencyStruct>(nTransparency, ofsTransparency, br);
                UVAnimations = ReadStructList<UVAnimationStruct>(nUVAnimation, ofsUVAnimation, br);
                TextureReplace = ReadStructList<TextureReplaceStruct>(nTexReplace, ofsTexReplace, br);
                RenderFlags = ReadStructList<RenderFlagStruct>(nRenderFlags, ofsRenderFlags, br);
                BoneLookupTable = ReadStructList<BoneLookupTableStruct>(nBoneLookupTable, ofsBoneLookupTable, br);
                TextureLookup = ReadStructList<TextureLookupStruct>(nTexLookup, ofsTexLookup, br);
                TransparencyLookup = ReadStructList<TransparencyLookupStruct>(nTransLookup, ofsTranslookup, br);
                UVAnimLookup = ReadStructList<UVAnimLookupStruct>(nUVAnimLookup, ofsUVAnimLookup, br);
                BoundingTriangles = ReadStructList<BoundingTriangleStruct>(nBoundingTriangles, ofsBoundingTriangles, br);
                BoundingVertices = ReadStructList<BoundingVertexStruct>(nBoundingVertices, ofsBoundingVertices, br);
                BoundingNormals = ReadStructList<BoundingNormalStruct>(nBoundingNormals, ofsBoundingNormals, br);
                Attachments = ReadStructList<AttachmentStruct>(nAttachments, ofsAttachments, br);
                AttachLookup = ReadStructList<AttachLookupStruct>(nAttachLookup, ofsAttachLookup, br);
                Events = ReadStructList<EventStruct>(nEvents, ofsEvents, br);
                Lights = ReadStructList<LightStruct>(nLights, ofsLights, br);
                Cameras = ReadStructList<CameraStruct>(nCameras, ofsCameras, br);
                CameraLookup = ReadStructList<CameraLookupStruct>(nCameraLookup, ofsCameraLookup, br);
                RibbonEmitters = ReadStructList<RibbonEmitterStruct>(nRibbonEmitters, ofsRibbonEmitters, br);
                ParticleEmitters = ReadStructList<ParticleEmitterStruct>(nParticleEmitters, ofsParticleEmitters, br);
            }
        }

        private List<T> ReadStructList<T>(uint count, uint offset, BinaryReader br) where T : struct
        {
            br.BaseStream.Position = offset;
            List<T> list = [];

            for (var i = 0; i < count; i++)
                list.Add(br.ReadStruct<T>());

            return list;
        }

        private static List<TextureStruct> ReadTextures(uint count, uint offset, BinaryReader br)
        {
            br.BaseStream.Position = offset;
            var textures = new TextureStruct[count];

            for (var i = 0; i < count; i++)
            {
                textures[i].Type = (TextureType)br.ReadUInt32();
                textures[i].Flags = (TextureFlags)br.ReadUInt32();
                textures[i].Filename = "";

                var lenFilename = br.ReadUInt32();
                var ofsFilename = br.ReadUInt32();

                if (textures[i].Type == TextureType.None)
                {
                    if (ofsFilename >= 10)
                    {
                        var preFilenamePosition = br.BaseStream.Position; // probably a better way to do all this
                        br.BaseStream.Position = ofsFilename;
                        var filename = new string(br.ReadChars(int.Parse(lenFilename.ToString())));
                        filename = filename.Replace("\0", "");
                        if (!filename.Equals(""))
                        {
                            textures[i].Filename = filename;
                        }

                        br.BaseStream.Position = preFilenamePosition;
                    }
                }
            }

            return textures.ToList();
        }

        private List<AnimationStruct> ReadAnimations(uint nAnimations, uint ofsAnimations, BinaryReader br)
        {
            br.BaseStream.Position = ofsAnimations;
            Dictionary<ushort, AnimationStruct> animations = new Dictionary<ushort, AnimationStruct>();

            for (var i = 0; i < nAnimations; i++)
            {
                AnimationStruct animation = br.ReadStruct<AnimationStruct>();
                animations.TryAdd(animation.AnimationID, animation);
            }

            return animations.Values.ToList();
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        private static void UpdateHeaderInfo(BinaryWriter bw, long headerOffset, uint newLength, uint newOffset)
        {
            var pos = bw.BaseStream.Position;
            bw.BaseStream.Position = headerOffset;
            bw.Write(newLength);
            bw.Write(newOffset);
            bw.BaseStream.Position = pos;
        }

        /// <summary>
        /// Serializes the current object into a byte array.
        /// WARNING: The serializer just write back the original MD21 content!
        /// </summary>
        /// <returns>The serialized object.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write("MD20".ToCharArray());
                bw.Write(Version);

                bw.Write((uint)0); // lenModelname
                bw.Write((uint)0); // ofsModelname

                bw.Write((uint)Flags);

                bw.Write((uint)Sequences.Count);
                bw.Write((uint)0); // ofsSequences

                bw.Write((uint)Animations.Count);
                bw.Write((uint)0); // ofsAnimations

                bw.Write((uint)AnimationLookups.Count);
                bw.Write((uint)0); // ofsAnimationLookup    

                bw.Write((uint)0); // nBones
                bw.Write((uint)0); // ofsBones

                bw.Write((uint)0); // nKeyboneLookup
                bw.Write((uint)0); // ofsKeyboneLookup

                bw.Write((uint)0); // nVertices
                bw.Write((uint)0); // ofsVertices

                bw.Write(ViewCount);

                bw.Write((uint)0); // nColors
                bw.Write((uint)0); // ofsColors

                bw.Write((uint)0); // nTextures
                bw.Write((uint)0); // ofsTextures

                bw.Write((uint)0); // nTransparency
                bw.Write((uint)0); // ofsTransparency

                bw.Write((uint)0); // nUVAnimation
                bw.Write((uint)0); // ofsUVAnimation

                bw.Write((uint)0); // nTexReplace
                bw.Write((uint)0); // ofsTexReplace

                bw.Write((uint)0); // nRenderFlags
                bw.Write((uint)0); // ofsRenderFlags

                bw.Write((uint)0); // nBoneLookupTable
                bw.Write((uint)0); // ofsBoneLookupTable

                bw.Write((uint)0); // nTexLookup
                bw.Write((uint)0); // ofsTexLookup

                bw.Write((uint)0); // nUnk1
                bw.Write((uint)0); // ofsUnk1

                bw.Write((uint)0); // nTransLookup
                bw.Write((uint)0); // ofsTranslookup

                bw.Write((uint)0); // nUVAnimLookup
                bw.Write((uint)0); // ofsUVAnimLookup

                bw.WriteBoundingBox(BoundingBox, AxisConfiguration.Native);
                bw.Write(BoundingBoxRadius);

                bw.WriteBoundingBox(VertexBox, AxisConfiguration.Native);
                bw.Write(VertexBoxRadius);

                bw.Write((uint)0); // nBoundingTriangles
                bw.Write((uint)0); // ofsBoundingTriangles

                bw.Write((uint)0); // nBoundingVertices
                bw.Write((uint)0); // ofsBoundingVertices

                bw.Write((uint)0); // nBoundingNormals
                bw.Write((uint)0); // ofsBoundingNormals

                bw.Write((uint)0); // nAttachments
                bw.Write((uint)0); // ofsAttachments

                bw.Write((uint)0); // nAttachLookup
                bw.Write((uint)0); // ofsAttachLookup

                bw.Write((uint)0); // nEvents
                bw.Write((uint)0); // ofsEvents

                bw.Write((uint)0); // nLights
                bw.Write((uint)0); // ofsLights

                bw.Write((uint)0); // nCameras
                bw.Write((uint)0); // ofsCameras

                bw.Write((uint)0); // nCameraLookup
                bw.Write((uint)0); // ofsCameraLookup

                bw.Write((uint)0); // nRibbonEmitters
                bw.Write((uint)0); // ofsRibbonEmitters

                bw.Write((uint)0); // nParticleEmitters
                bw.Write((uint)0); // ofsParticleEmitters

                // Model with flag 8 have extra field
                if (Flags.HasFlag(MD21Flags.UseTextureCombinerCombos))
                {
                    bw.Write((uint)0); // nUnk2
                    bw.Write((uint)0); // ofsUnk2
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    var ofsModelname = ms.Position;
                    bw.WriteNullTerminatedString(Name);

                    UpdateHeaderInfo(bw, 8, (uint)Name.Length + 1, (uint)ofsModelname);
                }

                if (Sequences.Count > 0)
                {
                    var ofsSequences = ms.Position;
                    foreach (var sequence in Sequences)
                        bw.WriteStruct(sequence);
                    UpdateHeaderInfo(bw, 20, (uint)Sequences.Count, (uint)ofsSequences);
                }

                if (Animations.Count > 0)
                {
                    var ofsAnimations = ms.Position;
                    foreach (var animation in Animations)
                        bw.WriteStruct(animation);
                    UpdateHeaderInfo(bw, 28, (uint)Animations.Count, (uint)ofsAnimations);
                }

                if (AnimationLookups.Count > 0)
                {
                    var ofsAnimationLookup = ms.Position;
                    foreach (var animationLookup in AnimationLookups)
                        bw.WriteStruct(animationLookup);
                    UpdateHeaderInfo(bw, 36, (uint)AnimationLookups.Count, (uint)ofsAnimationLookup);
                }

                if (Bones.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Bones.Count}x Bones struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsBones = ms.Position;
                    foreach (var bone in Bones)
                        bw.WriteStruct(bone);
                    UpdateHeaderInfo(bw, 44, (uint)Bones.Count, (uint)ofsBones);
                }

                if (KeyBoneLookup.Count > 0)
                {
                    var ofsKeyboneLookup = ms.Position;
                    foreach (var keyBoneLookup in KeyBoneLookup)
                        bw.WriteStruct(keyBoneLookup);
                    UpdateHeaderInfo(bw, 52, (uint)KeyBoneLookup.Count, (uint)ofsKeyboneLookup);
                }

                if (Vertices.Count > 0)
                {
                    var ofsVertices = ms.Position;
                    foreach (var vertice in Vertices)
                        bw.WriteStruct(vertice);
                    UpdateHeaderInfo(bw, 60, (uint)Vertices.Count, (uint)ofsVertices);
                }

                if (Colors.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Colors.Count}x Colors struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsColors = ms.Position;
                    foreach (var color in Colors)
                        bw.WriteStruct(color);
                    UpdateHeaderInfo(bw, 72, (uint)Colors.Count, (uint)ofsColors);
                }

                if (Textures.Count > 0)
                {
                    var ofsTextures = ms.Position;
                    foreach (var texture in Textures)
                    {
                        bw.Write((uint)texture.Type);
                        bw.Write((uint)texture.Flags);

                        if (!string.IsNullOrEmpty(texture.Filename))
                        {
                            // TODO: Filename chunk writing for older M2s
                            throw new NotImplementedException();
                        }
                        else
                        {
                            bw.Write((uint)0);
                            bw.Write((uint)0);
                        }
                    }

                    UpdateHeaderInfo(bw, 80, (uint)Textures.Count, (uint)ofsTextures);
                }

                if (Transparency.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Transparency.Count}x Transparency struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsTransparency = ms.Position;
                    foreach (var transparency in Transparency)
                        bw.WriteStruct(transparency);

                    UpdateHeaderInfo(bw, 88, (uint)Transparency.Count, (uint)ofsTransparency);
                }

                if (UVAnimations.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {UVAnimations.Count}x UVAnimations struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsUVAnimations = ms.Position;
                    foreach (var uvAnimation in UVAnimations)
                        bw.WriteStruct(uvAnimation);

                    UpdateHeaderInfo(bw, 96, (uint)UVAnimations.Count, (uint)ofsUVAnimations);
                }

                if (TextureReplace.Count > 0)
                {
                    var ofsTexReplace = ms.Position;
                    foreach (var textureReplace in TextureReplace)
                        bw.WriteStruct(textureReplace);

                    UpdateHeaderInfo(bw, 104, (uint)TextureReplace.Count, (uint)ofsTexReplace);
                }

                if (RenderFlags.Count > 0)
                {
                    var ofsRenderFlags = ms.Position;
                    foreach (var renderFlag in RenderFlags)
                        bw.WriteStruct(renderFlag);
                    UpdateHeaderInfo(bw, 112, (uint)RenderFlags.Count, (uint)ofsRenderFlags);
                }

                if (BoneLookupTable.Count > 0)
                {
                    var ofsBoneLookupTable = ms.Position;
                    foreach (var boneLookupTable in BoneLookupTable)
                        bw.WriteStruct(boneLookupTable);
                    UpdateHeaderInfo(bw, 120, (uint)BoneLookupTable.Count, (uint)ofsBoneLookupTable);
                }

                if (TextureLookup.Count > 0)
                {
                    var ofsTexLookup = ms.Position;
                    foreach (var textureLookup in TextureLookup)
                        bw.WriteStruct(textureLookup);
                    UpdateHeaderInfo(bw, 128, (uint)TextureLookup.Count, (uint)ofsTexLookup);
                }

                // TODO: Unk1?

                if (TransparencyLookup.Count > 0)
                {
                    var ofsTranslookup = ms.Position;
                    foreach (var transparencyLookup in TransparencyLookup)
                        bw.WriteStruct(transparencyLookup);
                    UpdateHeaderInfo(bw, 144, (uint)TransparencyLookup.Count, (uint)ofsTranslookup);
                }

                if (UVAnimLookup.Count > 0)
                {
                    var ofsUVAnimLookup = ms.Position;
                    foreach (var uvAnimLookup in UVAnimLookup)
                        bw.WriteStruct(uvAnimLookup);
                    UpdateHeaderInfo(bw, 152, (uint)UVAnimLookup.Count, (uint)ofsUVAnimLookup);
                }

                // BoundingBox etc is already written and adds 56 bytes

                if (BoundingTriangles.Count > 0)
                {
                    var ofsBoundingTriangles = ms.Position;
                    foreach (var boundingTriangle in BoundingTriangles)
                        bw.WriteStruct(boundingTriangle);
                    UpdateHeaderInfo(bw, 216, (uint)BoundingTriangles.Count, (uint)ofsBoundingTriangles);
                }

                if (BoundingVertices.Count > 0)
                {
                    var ofsBoundingVertices = ms.Position;
                    foreach (var boundingVertex in BoundingVertices)
                        bw.WriteStruct(boundingVertex);
                    UpdateHeaderInfo(bw, 224, (uint)BoundingVertices.Count, (uint)ofsBoundingVertices);
                }

                if (BoundingNormals.Count > 0)
                {
                    var ofsBoundingNormals = ms.Position;
                    foreach (var boundingNormal in BoundingNormals)
                        bw.WriteStruct(boundingNormal);
                    UpdateHeaderInfo(bw, 232, (uint)BoundingNormals.Count, (uint)ofsBoundingNormals);
                }

                if (Attachments.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Attachments.Count}x Attachments struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsAttachments = ms.Position;
                    foreach (var attachment in Attachments)
                        bw.WriteStruct(attachment);
                    UpdateHeaderInfo(bw, 240, (uint)Attachments.Count, (uint)ofsAttachments);
                }

                if (AttachLookup.Count > 0)
                {
                    var ofsAttachLookup = ms.Position;
                    foreach (var attachLookup in AttachLookup)
                        bw.WriteStruct(attachLookup);
                    UpdateHeaderInfo(bw, 248, (uint)AttachLookup.Count, (uint)ofsAttachLookup);
                }

                if (Events.Count > 0)
                {
                    var ofsEvents = ms.Position;
                    foreach (var @event in Events)
                        bw.WriteStruct(@event);
                    UpdateHeaderInfo(bw, 256, (uint)Events.Count, (uint)ofsEvents);
                }

                if (Lights.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Lights.Count}x Lights struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsLights = ms.Position;
                    foreach (var light in Lights)
                        bw.WriteStruct(light);
                    UpdateHeaderInfo(bw, 264, (uint)Lights.Count, (uint)ofsLights);
                }

                if (Cameras.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {Cameras.Count}x Cameras struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsCameras = ms.Position;
                    foreach (var camera in Cameras)
                        bw.WriteStruct(camera);
                    UpdateHeaderInfo(bw, 272, (uint)Cameras.Count, (uint)ofsCameras);
                }

                if (CameraLookup.Count > 0)
                {
                    var ofsCameraLookup = ms.Position;
                    foreach (var cameraLookup in CameraLookup)
                        bw.WriteStruct(cameraLookup);
                    UpdateHeaderInfo(bw, 280, (uint)CameraLookup.Count, (uint)ofsCameraLookup);
                }

                if (RibbonEmitters.Count > 0)
                {
                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {RibbonEmitters.Count}x RibbonEmitters struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsRibbonEmitters = ms.Position;
                    foreach (var ribbonEmitter in RibbonEmitters)
                        bw.WriteStruct(ribbonEmitter);
                    UpdateHeaderInfo(bw, 288, (uint)RibbonEmitters.Count, (uint)ofsRibbonEmitters);
                }

                if (ParticleEmitters.Count > 0)
                {
                    throw new NotImplementedException(); // MD21Entry.ParticleEmitterStruct is NYI

                    // TODO: ABlock writing
                    if (Settings.logLevel >= LogLevel.Warning)
                        Console.WriteLine($"Attempted to write {ParticleEmitters.Count}x ParticleEmitters struct(s) containing an ABlock. This is not yet supported and likely will cause reading errors.");

                    var ofsParticleEmitters = ms.Position;
                    foreach (var particleEmitter in ParticleEmitters)
                        bw.WriteStruct(particleEmitter);

                    UpdateHeaderInfo(bw, 296, (uint)ParticleEmitters.Count, (uint)ofsParticleEmitters);
                }

                return ms.ToArray();
            }
        }
    }
}