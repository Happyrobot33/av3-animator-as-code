﻿using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace AnimatorAsCode.Framework
{
    /// <summary> Animator as Code Framework </summary>
    public partial class Aac
    {
        /// <summary> Create a new Animator as Code based on the configuration </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>AacFlBase</returns>
        public static AacFlBase Create(AacConfiguration configuration)
        {
            return new AacFlBase(configuration);
        }

        internal static AnimationClip NewClip(AacConfiguration component, string suffix)
        {
            return RegisterClip(component, suffix, new AnimationClip());
        }

        internal static AnimationClip RegisterClip(
            AacConfiguration component,
            string suffix,
            AnimationClip clip
        )
        {
            clip.name =
                "AAC_" + suffix + "_" + component.AssetKey + "_" + Random.Range(0, Int32.MaxValue); // FIXME animation name conflict
            clip.hideFlags = HideFlags.None;
            AssetDatabase.AddObjectToAsset(clip, component.AssetContainer);
            return clip;
        }

        internal static BlendTree NewBlendTreeAsRaw(AacConfiguration component, string suffix)
        {
            var clip = new BlendTree();
            clip.name =
                "zAutogenerated__"
                + component.AssetKey
                + "__"
                + suffix
                + "_"
                + Random.Range(0, Int32.MaxValue); // FIXME animation name conflict
            clip.hideFlags = HideFlags.None;
            AssetDatabase.AddObjectToAsset(clip, component.AssetContainer);
            return clip;
        }

        internal static EditorCurveBinding Binding(
            AacConfiguration component,
            Type type,
            Transform transform,
            string propertyName
        )
        {
            return new EditorCurveBinding
            {
                path = ResolveRelativePath(component.AnimatorRoot, transform),
                type = type,
                propertyName = propertyName
            };
        }

        internal static AnimationCurve OneFrame(float desiredValue)
        {
            return AnimationCurve.Constant(0f, 1 / 60f, desiredValue);
        }

        internal static AnimationCurve ConstantSeconds(float seconds, float desiredValue)
        {
            return AnimationCurve.Constant(0f, seconds, desiredValue);
        }

        internal static string ResolveRelativePath(Transform avatar, Transform item)
        {
            if (item.parent != avatar && item.parent != null)
            {
                return ResolveRelativePath(avatar, item.parent) + "/" + item.name;
            }

            return item.name;
        }

        internal static EditorCurveBinding ToSubBinding(EditorCurveBinding binding, string suffix)
        {
            return new EditorCurveBinding
            {
                path = binding.path,
                type = binding.type,
                propertyName = binding.propertyName + "." + suffix
            };
        }
    }

    public partial struct AacConfiguration
    {
        public string SystemName;
        public Transform AnimatorRoot;
        public Transform DefaultValueRoot;
        public AnimatorController AssetContainer;
        public string AssetKey;
        public IAacDefaultsProvider DefaultsProvider;
    }

    public partial struct AacFlLayer
    {
        private readonly AnimatorController _animatorController;
        private readonly AacConfiguration _configuration;
        private readonly string _fullLayerName;
        private readonly AacStateMachine _stateMachine;

        internal AacFlLayer(
            AnimatorController animatorController,
            AacConfiguration configuration,
            AacStateMachine stateMachine,
            string fullLayerName
        )
        {
            _animatorController = animatorController;
            _configuration = configuration;
            _fullLayerName = fullLayerName;
            _stateMachine = stateMachine;
        }

        /// <summary>
        /// Create a new state
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <returns>AacFlState</returns>
        public AacFlState NewState(string name)
        {
            var lastState = _stateMachine.LastStatePosition();
            var state = _stateMachine.NewState(name, 0, 0).Shift(lastState, 0, 1);
            return state;
        }

        /// <summary>
        /// Create a new state with a position
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <param name="x">X position of the state</param>
        /// <param name="y">Y position of the state</param>
        /// <returns>AacFlState</returns>
        public AacFlState NewState(string name, int x, int y)
        {
            return _stateMachine.NewState(name, x, y);
        }

        /// <summary>
        /// Create transition from the layers any state to the given state
        /// </summary>
        /// <param name="destination">Destination state</param>
        /// <returns>AacFlAnyStateTransition</returns>
        public AacFlTransition AnyTransitionsTo(AacFlState destination)
        {
            return _stateMachine.AnyTransitionsTo(destination);
        }

        /// <summary>
        /// Create transition from the layers entry state to the given state
        /// </summary>
        /// <param name="destination">Destination state</param>
        /// <returns>AacFlEntryTransition</returns>
        public AacFlEntryTransition EntryTransitionsTo(AacFlState destination)
        {
            return _stateMachine.EntryTransitionsTo(destination);
        }

        public AacFlBoolParameter BoolParameter(string parameterName) =>
            _stateMachine.BackingAnimator().BoolParameter(parameterName);

        public AacFlBoolParameter TriggerParameterAsBool(string parameterName) =>
            _stateMachine.BackingAnimator().TriggerParameter(parameterName);

        public AacFlFloatParameter FloatParameter(string parameterName) =>
            _stateMachine.BackingAnimator().FloatParameter(parameterName);

        public AacFlIntParameter IntParameter(string parameterName) =>
            _stateMachine.BackingAnimator().IntParameter(parameterName);

        public AacFlBoolParameterGroup BoolParameters(params string[] parameterNames) =>
            _stateMachine.BackingAnimator().BoolParameters(parameterNames);

        public AacFlBoolParameterGroup TriggerParametersAsBools(params string[] parameterNames) =>
            _stateMachine.BackingAnimator().TriggerParameters(parameterNames);

        public AacFlFloatParameterGroup FloatParameters(params string[] parameterNames) =>
            _stateMachine.BackingAnimator().FloatParameters(parameterNames);

        public AacFlIntParameterGroup IntParameters(params string[] parameterNames) =>
            _stateMachine.BackingAnimator().IntParameters(parameterNames);

        public AacFlBoolParameterGroup BoolParameters(params AacFlBoolParameter[] parameters) =>
            _stateMachine.BackingAnimator().BoolParameters(parameters);

        public AacFlBoolParameterGroup TriggerParametersAsBools(
            params AacFlBoolParameter[] parameters
        ) => _stateMachine.BackingAnimator().TriggerParameters(parameters);

        public AacFlFloatParameterGroup FloatParameters(params AacFlFloatParameter[] parameters) =>
            _stateMachine.BackingAnimator().FloatParameters(parameters);

        public AacFlIntParameterGroup IntParameters(params AacFlIntParameter[] parameters) =>
            _stateMachine.BackingAnimator().IntParameters(parameters);

        public AacAv3 Av3() => new AacAv3(_stateMachine.BackingAnimator());

        public void OverrideValue(AacFlBoolParameter toBeForced, bool value)
        {
            var parameters = _animatorController.parameters;
            foreach (var param in parameters)
            {
                if (param.name == toBeForced.Name)
                {
                    param.defaultBool = value;
                }
            }

            _animatorController.parameters = parameters;
        }

        public void OverrideValue(AacFlFloatParameter toBeForced, float value)
        {
            var parameters = _animatorController.parameters;
            foreach (var param in parameters)
            {
                if (param.name == toBeForced.Name)
                {
                    param.defaultFloat = value;
                }
            }

            _animatorController.parameters = parameters;
        }

        public void OverrideValue(AacFlIntParameter toBeForced, int value)
        {
            var parameters = _animatorController.parameters;
            foreach (var param in parameters)
            {
                if (param.name == toBeForced.Name)
                {
                    param.defaultInt = value;
                }
            }

            _animatorController.parameters = parameters;
        }

        public AacFlLayer WithAvatarMask(AvatarMask avatarMask)
        {
            var finalFullLayerName = _fullLayerName;
            _animatorController.layers = _animatorController.layers
                .Select(layer =>
                {
                    if (layer.name == finalFullLayerName)
                    {
                        layer.avatarMask = avatarMask;
                    }

                    return layer;
                })
                .ToArray();

            return this;
        }

        public void WithAvatarMaskNoTransforms()
        {
            ResolveAvatarMask(new Transform[0]);
        }

        public void ResolveAvatarMask(Transform[] paths)
        {
            // FIXME: Fragile
            var avatarMask = new AvatarMask();
            avatarMask.name =
                "zAutogenerated__"
                + _configuration.AssetKey
                + "_"
                + _fullLayerName
                + "__AvatarMask";
            avatarMask.hideFlags = HideFlags.None;

            if (paths.Length == 0)
            {
                avatarMask.transformCount = 1;
                avatarMask.SetTransformActive(0, false);
                avatarMask.SetTransformPath(0, "_ignored");
            }
            else
            {
                avatarMask.transformCount = paths.Length;
                for (var index = 0; index < paths.Length; index++)
                {
                    var transform = paths[index];
                    avatarMask.SetTransformActive(index, true);
                    avatarMask.SetTransformPath(
                        index,
                        Aac.ResolveRelativePath(_configuration.AnimatorRoot, transform)
                    );
                }
            }

            for (int i = 0; i < (int)AvatarMaskBodyPart.LastBodyPart; i++)
            {
                avatarMask.SetHumanoidBodyPartActive((AvatarMaskBodyPart)i, false);
            }

            AssetDatabase.AddObjectToAsset(avatarMask, _animatorController);

            WithAvatarMask(avatarMask);
        }
    }

    public partial class AacFlBase
    {
        private readonly AacConfiguration _configuration;

        internal AacFlBase(AacConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AacFlClip NewClip()
        {
            var clip = Aac.NewClip(_configuration, Guid.NewGuid().ToString());
            return new AacFlClip(_configuration, clip);
        }

        public AacFlClip CopyClip(AnimationClip originalClip)
        {
            var newClip = UnityEngine.Object.Instantiate(originalClip);
            var clip = Aac.RegisterClip(_configuration, Guid.NewGuid().ToString(), newClip);
            return new AacFlClip(_configuration, clip);
        }

        public BlendTree NewBlendTreeAsRaw()
        {
            return Aac.NewBlendTreeAsRaw(_configuration, Guid.NewGuid().ToString());
        }

        public AacFlClip NewClip(string name)
        {
            var clip = Aac.NewClip(_configuration, name);
            return new AacFlClip(_configuration, clip);
        }

        public AacFlClip DummyClipLasting(float numberOf, AacFlUnit unit)
        {
            var dummyClip = Aac.NewClip(
                _configuration,
                $"D({numberOf} {Enum.GetName(typeof(AacFlUnit), unit)})"
            );

            var duration = unit == AacFlUnit.Frames ? numberOf / 60f : numberOf;
            return new AacFlClip(_configuration, dummyClip).Animating(
                clip =>
                    clip.Animates("_ignored", typeof(GameObject), "m_IsActive")
                        .WithUnit(
                            unit,
                            keyframes => keyframes.Constant(0, 0f).Constant(duration, 0f)
                        )
            );
        }

        public AacFlLayer CreateMainArbitraryControllerLayer(AnimatorController controller) =>
            DoCreateLayer(
                controller,
                _configuration.DefaultsProvider.ConvertLayerName(_configuration.SystemName)
            );

        public AacFlLayer CreateSupportingArbitraryControllerLayer(
            AnimatorController controller,
            string suffix
        ) =>
            DoCreateLayer(
                controller,
                _configuration.DefaultsProvider.ConvertLayerNameWithSuffix(
                    _configuration.SystemName,
                    suffix
                )
            );

        public AacFlLayer CreateFirstArbitraryControllerLayer(AnimatorController controller) =>
            DoCreateLayer(controller, controller.layers[0].name);

        private AacFlLayer DoCreateLayer(AnimatorController animator, string layerName)
        {
            var ag = new AacAnimatorGenerator(
                animator,
                CreateEmptyClip().Clip,
                _configuration.DefaultsProvider
            );
            var machine = ag.CreateOrClearLayerAtSameIndex(layerName, 1f);

            return new AacFlLayer(animator, _configuration, machine, layerName);
        }

        private AacFlClip CreateEmptyClip()
        {
            var emptyClip = DummyClipLasting(1, AacFlUnit.Frames);
            return emptyClip;
        }

        public AacVrcAssetLibrary VrcAssets()
        {
            return new AacVrcAssetLibrary();
        }

        public void ClearPreviousAssets()
        {
            var allSubAssets = AssetDatabase.LoadAllAssetsAtPath(
                AssetDatabase.GetAssetPath(_configuration.AssetContainer)
            );
            foreach (var subAsset in allSubAssets)
            {
                if (
                    subAsset != _configuration.AssetContainer
                    && (
                        subAsset is AnimationClip || subAsset is BlendTree || subAsset is AvatarMask
                    )
                )
                {
                    //make sure not null
                    if (subAsset != null)
                    {
                        AssetDatabase.RemoveObjectFromAsset(subAsset);
                    }
                }
            }
        }
    }

    /// <summary>
    /// AacAv3 is a class that provides access to specifics of the VRChat SDK3 avatar system.
    /// </summary>
    public partial class AacAv3
    {
        private readonly AacBackingAnimator _backingAnimator;

        internal AacAv3(AacBackingAnimator backingAnimator)
        {
            _backingAnimator = backingAnimator;
        }

        // ReSharper disable InconsistentNaming
        public AacFlBoolParameter IsLocal => _backingAnimator.BoolParameter("IsLocal");
        public AacFlEnumIntParameter<Av3Viseme> Viseme =>
            _backingAnimator.EnumParameter<Av3Viseme>("Viseme");
        public AacFlEnumIntParameter<Av3Gesture> GestureLeft =>
            _backingAnimator.EnumParameter<Av3Gesture>("GestureLeft");
        public AacFlEnumIntParameter<Av3Gesture> GestureRight =>
            _backingAnimator.EnumParameter<Av3Gesture>("GestureRight");
        public AacFlFloatParameter GestureLeftWeight =>
            _backingAnimator.FloatParameter("GestureLeftWeight");
        public AacFlFloatParameter GestureRightWeight =>
            _backingAnimator.FloatParameter("GestureRightWeight");
        public AacFlFloatParameter AngularY => _backingAnimator.FloatParameter("AngularY");
        public AacFlFloatParameter VelocityX => _backingAnimator.FloatParameter("VelocityX");
        public AacFlFloatParameter VelocityY => _backingAnimator.FloatParameter("VelocityY");
        public AacFlFloatParameter VelocityZ => _backingAnimator.FloatParameter("VelocityZ");
        public AacFlFloatParameter Upright => _backingAnimator.FloatParameter("Upright");
        public AacFlBoolParameter Grounded => _backingAnimator.BoolParameter("Grounded");
        public AacFlBoolParameter Seated => _backingAnimator.BoolParameter("Seated");
        public AacFlBoolParameter AFK => _backingAnimator.BoolParameter("AFK");
        public AacFlIntParameter TrackingType => _backingAnimator.IntParameter("TrackingType");
        public AacFlIntParameter VRMode => _backingAnimator.IntParameter("VRMode");
        public AacFlBoolParameter MuteSelf => _backingAnimator.BoolParameter("MuteSelf");
        public AacFlBoolParameter InStation => _backingAnimator.BoolParameter("InStation");
        public AacFlFloatParameter Voice => _backingAnimator.FloatParameter("Voice");

        // ReSharper restore InconsistentNaming

        public IAacFlCondition ItIsRemote() => IsLocal.IsFalse();

        public IAacFlCondition ItIsLocal() => IsLocal.IsTrue();

        public enum Av3Gesture
        {
            // Specify all the values explicitly because they should be dictated by VRChat, not enumeration order.
            Neutral = 0,
            Fist = 1,
            HandOpen = 2,
            Fingerpoint = 3,
            Victory = 4,
            RockNRoll = 5,
            HandGun = 6,
            ThumbsUp = 7
        }

        public enum Av3Viseme
        {
            // Specify all the values explicitly because they should be dictated by VRChat, not enumeration order.
            // ReSharper disable InconsistentNaming
            sil = 0,
            pp = 1,
            ff = 2,
            th = 3,
            dd = 4,
            kk = 5,
            ch = 6,
            ss = 7,
            nn = 8,
            rr = 9,
            aa = 10,
            e = 11,
            ih = 12,
            oh = 13,
            ou = 14
            // ReSharper restore InconsistentNaming
        }
    }

    public partial class AacVrcAssetLibrary
    {
        public AvatarMask LeftHandAvatarMask()
        {
            return AssetDatabase.LoadAssetAtPath<AvatarMask>(
                "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Left.mask"
            );
        }

        public AvatarMask RightHandAvatarMask()
        {
            return AssetDatabase.LoadAssetAtPath<AvatarMask>(
                "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Right.mask"
            );
        }

        public AnimationClip ProxyForGesture(AacAv3.Av3Gesture gesture, bool masculine)
        {
            return AssetDatabase.LoadAssetAtPath<AnimationClip>(
                "Assets/VRCSDK/Examples3/Animation/ProxyAnim/"
                    + ResolveProxyFilename(gesture, masculine)
            );
        }

        private static string ResolveProxyFilename(AacAv3.Av3Gesture gesture, bool masculine)
        {
            switch (gesture)
            {
                case AacAv3.Av3Gesture.Neutral:
                    return masculine ? "proxy_hands_idle.anim" : "proxy_hands_idle2.anim";
                case AacAv3.Av3Gesture.Fist:
                    return "proxy_hands_fist.anim";
                case AacAv3.Av3Gesture.HandOpen:
                    return "proxy_hands_open.anim";
                case AacAv3.Av3Gesture.Fingerpoint:
                    return "proxy_hands_point.anim";
                case AacAv3.Av3Gesture.Victory:
                    return "proxy_hands_peace.anim";
                case AacAv3.Av3Gesture.RockNRoll:
                    return "proxy_hands_rock.anim";
                case AacAv3.Av3Gesture.HandGun:
                    return "proxy_hands_gun.anim";
                case AacAv3.Av3Gesture.ThumbsUp:
                    return "proxy_hands_thumbs_up.anim";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gesture), gesture, null);
            }
        }
    }

    public partial class AacAnimatorRemoval
    {
        private readonly AnimatorController _animatorController;

        public AacAnimatorRemoval(AnimatorController animatorController)
        {
            _animatorController = animatorController;
        }

        public void RemoveLayer(string layerName)
        {
            var index = FindIndexOf(layerName);
            if (index == -1)
                return;

            _animatorController.RemoveLayer(index);
        }

        private int FindIndexOf(string layerName)
        {
            return _animatorController.layers.ToList().FindIndex(layer => layer.name == layerName);
        }
    }

    public partial class AacAnimatorGenerator
    {
        private readonly AnimatorController _animatorController;
        private readonly AnimationClip _emptyClip;
        private readonly IAacDefaultsProvider _defaultsProvider;

        internal AacAnimatorGenerator(
            AnimatorController animatorController,
            AnimationClip emptyClip,
            IAacDefaultsProvider defaultsProvider
        )
        {
            _animatorController = animatorController;
            _emptyClip = emptyClip;
            _defaultsProvider = defaultsProvider;
        }

        internal void CreateParamsAsNeeded(params AacFlParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                switch (parameter)
                {
                    case AacFlIntParameter _:
                        CreateParamIfNotExists(parameter.Name, AnimatorControllerParameterType.Int);
                        break;
                    case AacFlFloatParameter _:
                        CreateParamIfNotExists(
                            parameter.Name,
                            AnimatorControllerParameterType.Float
                        );
                        break;
                    case AacFlBoolParameter _:
                        CreateParamIfNotExists(
                            parameter.Name,
                            AnimatorControllerParameterType.Bool
                        );
                        break;
                }
            }
        }

        internal void CreateTriggerParamsAsNeeded(params AacFlBoolParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                CreateParamIfNotExists(parameter.Name, AnimatorControllerParameterType.Trigger);
            }
        }

        private void CreateParamIfNotExists(string paramName, AnimatorControllerParameterType type)
        {
            if (
                _animatorController.parameters.FirstOrDefault(param => param.name == paramName)
                == null
            )
            {
                _animatorController.AddParameter(paramName, type);
            }
        }

        // DEPRECATED: This causes the editor window to glitch by deselecting, which is jarring for experimentation
        internal AacStateMachine CreateOrRemakeLayerAtSameIndex(
            string layerName,
            float weightWhenCreating,
            AvatarMask maskWhenCreating = null
        )
        {
            var originalIndexToPreserveOrdering = FindIndexOf(layerName);
            if (originalIndexToPreserveOrdering != -1)
            {
                _animatorController.RemoveLayer(originalIndexToPreserveOrdering);
            }

            AddLayerWithWeight(layerName, weightWhenCreating, maskWhenCreating);
            if (originalIndexToPreserveOrdering != -1)
            {
                var items = _animatorController.layers.ToList();
                var last = items[items.Count - 1];
                items.RemoveAt(items.Count - 1);
                items.Insert(originalIndexToPreserveOrdering, last);
                _animatorController.layers = items.ToArray();
            }

            var layer = TryGetLayer(layerName);
            var machinist = new AacStateMachine(
                layer.stateMachine,
                _emptyClip,
                new AacBackingAnimator(this),
                _defaultsProvider
            );
            return machinist
                .WithAnyStatePosition(0, 7)
                .WithEntryPosition(0, -1)
                .WithExitPosition(7, -1);
        }

        internal AacStateMachine CreateOrClearLayerAtSameIndex(
            string layerName,
            float weightWhenCreating,
            AvatarMask maskWhenCreating = null
        )
        {
            var originalIndexToPreserveOrdering = FindIndexOf(layerName);
            if (originalIndexToPreserveOrdering != -1)
            {
                foreach (
                    var childAnimatorStateMachine in _animatorController.layers[
                        originalIndexToPreserveOrdering
                    ]
                        .stateMachine
                        .stateMachines
                )
                {
                    childAnimatorStateMachine.stateMachine.states = new ChildAnimatorState[0];
                    childAnimatorStateMachine.stateMachine.entryTransitions =
                        new AnimatorTransition[0];
                    childAnimatorStateMachine.stateMachine.anyStateTransitions =
                        new AnimatorStateTransition[0];
                }
                _animatorController.layers[originalIndexToPreserveOrdering]
                    .stateMachine
                    .stateMachines = new ChildAnimatorStateMachine[0];
                _animatorController.layers[originalIndexToPreserveOrdering].stateMachine.states =
                    new ChildAnimatorState[0];
                _animatorController.layers[originalIndexToPreserveOrdering]
                    .stateMachine
                    .entryTransitions = new AnimatorTransition[0];
                _animatorController.layers[originalIndexToPreserveOrdering]
                    .stateMachine
                    .anyStateTransitions = new AnimatorStateTransition[0];
            }
            else
            {
                _animatorController.AddLayer(_animatorController.MakeUniqueLayerName(layerName));
                originalIndexToPreserveOrdering = _animatorController.layers.Length - 1;
            }

            var layers = _animatorController.layers;
            layers[originalIndexToPreserveOrdering].avatarMask = maskWhenCreating;
            layers[originalIndexToPreserveOrdering].defaultWeight = weightWhenCreating;
            _animatorController.layers = layers;

            var layer = TryGetLayer(layerName);
            var machinist = new AacStateMachine(
                layer.stateMachine,
                _emptyClip,
                new AacBackingAnimator(this),
                _defaultsProvider
            );
            return machinist
                .WithAnyStatePosition(0, 7)
                .WithEntryPosition(0, -1)
                .WithExitPosition(7, -1);
        }

        private int FindIndexOf(string layerName)
        {
            return _animatorController.layers
                .ToList()
                .FindIndex(layer1 => layer1.name == layerName);
        }

        private AnimatorControllerLayer TryGetLayer(string layerName)
        {
            return _animatorController.layers.FirstOrDefault(it => it.name == layerName);
        }

        private void AddLayerWithWeight(
            string layerName,
            float weightWhenCreating,
            AvatarMask maskWhenCreating
        )
        {
            _animatorController.AddLayer(_animatorController.MakeUniqueLayerName(layerName));

            var mutatedLayers = _animatorController.layers;
            mutatedLayers[mutatedLayers.Length - 1].defaultWeight = weightWhenCreating;
            mutatedLayers[mutatedLayers.Length - 1].avatarMask = maskWhenCreating;
            _animatorController.layers = mutatedLayers;
        }
    }
}
