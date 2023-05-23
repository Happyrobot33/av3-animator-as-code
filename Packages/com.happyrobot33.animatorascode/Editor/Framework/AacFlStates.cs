﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
#if VRC_SDK_VRCSDK3_AVATARS
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;
#endif

using System.Reflection;

// ReSharper disable once CheckNamespace
namespace AnimatorAsCode.Framework
{
    internal class AacBackingAnimator
    {
        private readonly AacAnimatorGenerator _generator;

        /// <summary> Creates a new backing animator. </summary>
        /// <param name="animatorGenerator"> The generator that will be used to generate the animator. </param>
        public AacBackingAnimator(AacAnimatorGenerator animatorGenerator)
        {
            _generator = animatorGenerator;
        }

        /// <summary> Creates a new boolean parameter in the animator. </summary>
        /// <remarks> If the parameter already exists, it will effectively be returned. </remarks>
        /// <param name="parameterName"> The name of the parameter. </param>
        /// <returns> The created AAC parameter. </returns>
        public AacFlBoolParameter BoolParameter(string parameterName)
        {
            var result = AacFlBoolParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        /// <summary> Creates a new trigger parameter in the animator. </summary>
        /// <remarks> If the parameter already exists, it will effectively be returned. </remarks>
        /// <param name="parameterName"> The name of the parameter. </param>
        /// <returns> The created AAC parameter. </returns>
        public AacFlBoolParameter TriggerParameter(string parameterName)
        {
            var result = AacFlBoolParameter.Internally(parameterName);
            _generator.CreateTriggerParamsAsNeeded(result);
            return result;
        }

        /// <summary> Creates a new float parameter in the animator. </summary>
        /// <remarks> If the parameter already exists, it will effectively be returned. </remarks>
        /// <param name="parameterName"> The name of the parameter. </param>
        /// <returns> The created AAC parameter. </returns>
        public AacFlFloatParameter FloatParameter(string parameterName)
        {
            var result = AacFlFloatParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        /// <summary> Creates a new integer parameter in the animator. </summary>
        /// <remarks> If the parameter already exists, it will effectively be returned. </remarks>
        /// <param name="parameterName"> The name of the parameter. </param>
        /// <returns> The created AAC parameter. </returns>
        public AacFlIntParameter IntParameter(string parameterName)
        {
            var result = AacFlIntParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        /// <summary> Creates a new enum parameter in the animator. </summary>
        /// <remarks> If the parameter already exists, it will effectively be returned. </remarks>
        /// <param name="parameterName"> The name of the parameter. </param>
        /// <typeparam name="TEnum"> The enum type. </typeparam>
        /// <returns> The created AAC parameter. </returns>
        public AacFlEnumIntParameter<TEnum> EnumParameter<TEnum>(string parameterName)
            where TEnum : Enum
        {
            var result = AacFlEnumIntParameter<TEnum>.Internally<TEnum>(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        /// <summary> Creates a new boolean parameter group in the animator. </summary>
        /// <remarks> If the parameter(s) already exist(s), it will effectively be returned. </remarks>
        /// <param name="parameterNames"> The names of the parameters. </param>
        /// <returns> The created AAC parameter group. </returns>
        public AacFlBoolParameterGroup BoolParameters(params string[] parameterNames)
        {
            var result = AacFlBoolParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        /// <summary> Creates a new trigger parameter group in the animator. </summary>
        /// <remarks> If the parameter(s) already exist(s), it will effectively be returned. </remarks>
        /// <param name="parameterNames"> The names of the parameters. </param>
        /// <returns> The created AAC parameter group. </returns>
        public AacFlBoolParameterGroup TriggerParameters(params string[] parameterNames)
        {
            var result = AacFlBoolParameterGroup.Internally(parameterNames);
            _generator.CreateTriggerParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        /// <summary> Creates a new float parameter group in the animator. </summary>
        /// <remarks> If the parameter(s) already exist(s), it will effectively be returned. </remarks>
        /// <param name="parameterNames"> The names of the parameters. </param>
        /// <returns> The created AAC parameter group. </returns>
        public AacFlFloatParameterGroup FloatParameters(params string[] parameterNames)
        {
            var result = AacFlFloatParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        /// <summary> Creates a new integer parameter group in the animator. </summary>
        /// <remarks> If the parameter(s) already exist(s), it will effectively be returned. </remarks>
        /// <param name="parameterNames"> The names of the parameters. </param>
        /// <returns> The created AAC parameter group. </returns>
        public AacFlIntParameterGroup IntParameters(params string[] parameterNames)
        {
            var result = AacFlIntParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        /// <inheritdoc cref="BoolParameters(string[])"/>
        /// <param name="parameters"> The list of <see cref="AacFlBoolParameter"/> to use in the list. </param>
        public AacFlBoolParameterGroup BoolParameters(params AacFlBoolParameter[] parameters)
        {
            var result = AacFlBoolParameterGroup.Internally(
                parameters.Select(parameter => parameter.Name).ToArray()
            );
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }

        /// <inheritdoc cref="TriggerParameters(string[])"/>
        /// <param name="parameters"> The list of <see cref="AacFlBoolParameter"/> to use in the list. </param>
        public AacFlBoolParameterGroup TriggerParameters(params AacFlBoolParameter[] parameters)
        {
            var result = AacFlBoolParameterGroup.Internally(
                parameters.Select(parameter => parameter.Name).ToArray()
            );
            _generator.CreateTriggerParamsAsNeeded(parameters);
            return result;
        }

        /// <inheritdoc cref="FloatParameters(string[])"/>
        /// <param name="parameters"> The list of <see cref="AacFlFloatParameter"/> to use in the list. </param>
        public AacFlFloatParameterGroup FloatParameters(params AacFlFloatParameter[] parameters)
        {
            var result = AacFlFloatParameterGroup.Internally(
                parameters.Select(parameter => parameter.Name).ToArray()
            );
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }

        /// <inheritdoc cref="IntParameters(string[])"/>
        /// <param name="parameters"> The list of <see cref="AacFlIntParameter"/> to use in the list. </param>
        public AacFlIntParameterGroup IntParameters(params AacFlIntParameter[] parameters)
        {
            var result = AacFlIntParameterGroup.Internally(
                parameters.Select(parameter => parameter.Name).ToArray()
            );
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }
    }

    internal class AacStateMachine
    {
        private static readonly PropertyInfo PropPushUndo =
            typeof(AnimatorStateMachine).GetProperty(
                "pushUndo",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
        private readonly AnimatorStateMachine _machine;
        private readonly AnimationClip _emptyClip;
        private readonly AacBackingAnimator _backingAnimator;
        private readonly IAacDefaultsProvider _defaultsProvider;
        private readonly float _gridShiftX;
        private readonly float _gridShiftY;

        public AacStateMachine(
            AnimatorStateMachine machine,
            AnimationClip emptyClip,
            AacBackingAnimator backingAnimator,
            IAacDefaultsProvider defaultsProvider
        )
        {
            PropPushUndo.SetValue(machine, false);
            _machine = machine;
            _emptyClip = emptyClip;
            _backingAnimator = backingAnimator;
            _defaultsProvider = defaultsProvider;

            var grid = defaultsProvider.Grid();
            _gridShiftX = grid.x;
            _gridShiftY = grid.y;
        }

        internal AacBackingAnimator BackingAnimator()
        {
            return _backingAnimator;
        }

        /// <summary> Sets the entry state position. </summary>
        /// <inheritdoc cref="GridPosition(int,int)" path="/remarks"/>
        /// <param name="x"> The x position. </param>
        /// <param name="y"> The y position. </param>
        /// <returns> This instance. </returns>
        public AacStateMachine WithEntryPosition(int x, int y)
        {
            _machine.entryPosition = GridPosition(x, y);
            return this;
        }

        /// <summary> Sets the exit state position. </summary>
        /// <inheritdoc cref="GridPosition(int,int)" path="/remarks"/>
        /// <param name="x"> The x position. </param>
        /// <param name="y"> The y position. </param>
        /// <returns> This instance. </returns>
        public AacStateMachine WithExitPosition(int x, int y)
        {
            _machine.exitPosition = GridPosition(x, y);
            return this;
        }

        /// <summary> Sets the any state position. </summary>
        /// <inheritdoc cref="GridPosition(int,int)" path="/remarks"/>
        /// <param name="x"> The x position. </param>
        /// <param name="y"> The y position. </param>
        /// <returns> This instance. </returns>
        public AacStateMachine WithAnyStatePosition(int x, int y)
        {
            _machine.anyStatePosition = GridPosition(x, y);
            return this;
        }

        /// <summary> Creates a new state in this state machine. </summary>
        /// <inheritdoc cref="GridPosition(int,int)" path="/remarks"/>
        /// <param name="name"> The name of the state.</param>
        /// <param name="x"> The x position. </param>
        /// <param name="y"> The y position. </param>
        /// <returns> The created state. </returns>
        public AacFlState NewState(string name, int x, int y)
        {
            var state = _machine.AddState(name, GridPosition(x, y));
            _defaultsProvider.ConfigureState(state, _emptyClip);

            return new AacFlState(state, _machine, _defaultsProvider);
        }

        /// <summary> Creates a transition from the any state to the given state. </summary>
        /// <param name="destination"> The destination state. </param>
        /// <returns> The created transition. </returns>
        public AacFlTransition AnyTransitionsTo(AacFlState destination)
        {
            return AnyTransition(destination, _machine);
        }

        /// <summary> Creates a transition from the entry state to the given state. </summary>
        /// <param name="destination"> The destination state. </param>
        /// <returns> The created transition. </returns>
        public AacFlEntryTransition EntryTransitionsTo(AacFlState destination)
        {
            return EntryTransition(destination, _machine);
        }

        private AacFlTransition AnyTransition(
            AacFlState destination,
            AnimatorStateMachine animatorStateMachine
        )
        {
            return new AacFlTransition(
                ConfigureTransition(animatorStateMachine.AddAnyStateTransition(destination.State)),
                animatorStateMachine,
                null,
                destination.State
            );
        }

        private AnimatorStateTransition ConfigureTransition(AnimatorStateTransition transition)
        {
            _defaultsProvider.ConfigureTransition(transition);
            return transition;
        }

        private AacFlEntryTransition EntryTransition(
            AacFlState destination,
            AnimatorStateMachine animatorStateMachine
        )
        {
            return new AacFlEntryTransition(
                animatorStateMachine.AddEntryTransition(destination.State),
                animatorStateMachine,
                null,
                destination.State
            );
        }

        internal Vector2 LastStatePosition()
        {
            return _machine.states.Length > 0
                ? (Vector2)_machine.states.Last().position
                : Vector2.zero;
        }

        /// <summary> Returns the grid position for the given x and y. The grid position is calculated by multiplying the x and y with the grid shift. </summary>
        /// <remarks> Grid states are 200x10. </remarks>
        /// <param name="x"> The x position. </param>
        /// <param name="y"> The y position. </param>
        /// <returns> The grid position. </returns>
        private Vector3 GridPosition(int x, int y)
        {
            return new Vector3(x * _gridShiftX, y * _gridShiftY, 0);
        }
    }

    /// <summary> A state in a state machine. </summary>
    /// <inheritdoc cref="AacStateMachine.GridPosition(int,int)" path="/remarks"/>
    public class AacFlState
    {
        private static readonly PropertyInfo PropPushUndo = typeof(AnimatorState).GetProperty(
            "pushUndo",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        /// <summary> The underlying state. </summary>
        public readonly AnimatorState State;
        private readonly AnimatorStateMachine _machine;
        private readonly IAacDefaultsProvider _defaultsProvider;
        private readonly AacBackingAnimator _backingAnimator;

#if VRC_SDK_VRCSDK3_AVATARS
        private VRCAvatarParameterDriver _driver;
        private VRCAnimatorTrackingControl _tracking;
        private VRCAnimatorLocomotionControl _locomotionControl;
#endif

        /// <summary> Creates a new state. </summary>
        /// <param name="state"> The underlying state. </param>
        /// <param name="machine"> The state machine this state belongs to. </param>
        /// <param name="defaultsProvider"> The defaults provider. </param>
        public AacFlState(
            AnimatorState state,
            AnimatorStateMachine machine,
            IAacDefaultsProvider defaultsProvider
        )
        {
            PropPushUndo.SetValue(state, false);
            State = state;
            _machine = machine;
            _defaultsProvider = defaultsProvider;
        }

        /// <summary> Sets the position of this state to the left of another state. </summary>
        /// <param name="otherState"> The other state. </param>
        /// <returns> This instance. </returns>
        public AacFlState LeftOf(AacFlState otherState) => MoveNextTo(otherState, -1, 0);

        /// <summary> Sets the position of this state to the right of another state. </summary>
        /// <inheritdoc cref="LeftOf(AacFlState)"/>
        public AacFlState RightOf(AacFlState otherState) => MoveNextTo(otherState, 1, 0);

        /// <summary> Sets the position of this state to above another state. </summary>
        /// <inheritdoc cref="LeftOf(AacFlState)"/>
        public AacFlState Over(AacFlState otherState) => MoveNextTo(otherState, 0, -1);

        /// <summary> Sets the position of this state to below another state. </summary>
        /// <inheritdoc cref="LeftOf(AacFlState)"/>
        public AacFlState Under(AacFlState otherState) => MoveNextTo(otherState, 0, 1);

        /// <summary> Sets the position of this state to the left of the second to last state. </summary>
        /// <returns> This instance. </returns>
        public AacFlState LeftOf() => MoveNextTo(null, -1, 0);

        /// <summary> Sets the position of this state to the right of the second to last state. </summary>
        /// <inheritdoc cref="LeftOf()"/>
        public AacFlState RightOf() => MoveNextTo(null, 1, 0);

        /// <summary> Sets the position of this state to above the second to last state. </summary>
        /// <inheritdoc cref="LeftOf()"/>
        public AacFlState Over() => MoveNextTo(null, 0, -1);

        /// <summary> Sets the position of this state to below the second to last state. </summary>
        /// <inheritdoc cref="LeftOf()"/>
        public AacFlState Under() => MoveNextTo(null, 0, 1);

        /// <summary> Sets the position of this state relative to another state. </summary>
        /// <inheritdoc cref="AacStateMachine.GridPosition(int,int)" path="/remarks"/>
        /// <param name="otherState"> The other state. </param>
        /// <param name="shiftX"> The x shift. </param>
        /// <param name="shiftY"> The y shift. </param>
        /// <returns> This instance. </returns>
        public AacFlState Shift(AacFlState otherState, int shiftX, int shiftY) =>
            MoveNextTo(otherState, shiftX, shiftY);

        private AacFlState MoveNextTo(AacFlState otherStateOrSecondToLastWhenNull, int x, int y)
        {
            if (otherStateOrSecondToLastWhenNull == null)
            {
                var other = _machine.states[_machine.states.Length - 2];
                Shift((Vector2)other.position, x, y);

                return this;
            }
            else
            {
                var other = _machine.states.First(
                    animatorState => animatorState.state == otherStateOrSecondToLastWhenNull.State
                );
                Shift((Vector2)other.position, x, y);

                return this;
            }
        }

        /// <summary> Sets the position of this state relative to a position on the grid. </summary>
        /// <inheritdoc cref="AacStateMachine.GridPosition(int,int)" path="/remarks"/>
        /// <param name="otherPosition"> The other position. </param>
        /// <param name="shiftX"> The x shift. </param>
        /// <param name="shiftY"> The y shift. </param>
        /// <returns> This instance. </returns>
        [Obsolete("Use Shift(Vector2, int, int) instead.")]
        public AacFlState Shift(Vector3 otherPosition, int shiftX, int shiftY)
        {
            return Shift((Vector2)otherPosition, shiftX, shiftY);
        }

        /// <summary> Sets the position of this state relative to a position on the grid. </summary>
        /// <inheritdoc cref="AacStateMachine.GridPosition(int,int)" path="/remarks"/>
        /// <param name="otherPosition"> The other position. </param>
        /// <param name="shiftX"> The x shift. </param>
        /// <param name="shiftY"> The y shift. </param>
        /// <returns> This instance. </returns>
        public AacFlState Shift(Vector2 otherPosition, int shiftX, int shiftY)
        {
            Vector3 otherPositionV3 = new Vector3(otherPosition.x, otherPosition.y, 0);
            var states = _machine.states;
            for (var index = 0; index < states.Length; index++)
            {
                var childAnimatorState = states[index];
                if (childAnimatorState.state == State)
                {
                    var cms = childAnimatorState;
                    cms.position =
                        otherPositionV3
                        + new Vector3(
                            shiftX * _defaultsProvider.Grid().x,
                            shiftY * _defaultsProvider.Grid().y,
                            0
                        );
                    states[index] = cms;
                    break;
                }
            }
            _machine.states = states;
            return this;
        }

        /// <summary> Sets the animation clip of this state. </summary>
        /// <param name="clip"> The clip. </param>
        /// <returns> This instance. </returns>
        public AacFlState WithAnimation(Motion clip)
        {
            State.motion = clip;
            return this;
        }

        /// <inheritdoc cref="WithAnimation(Motion)"/>
        public AacFlState WithAnimation(AacFlClip clip)
        {
            State.motion = clip.Clip;
            return this;
        }

        /// <summary> Create a transition from this state to another state. </summary>
        /// <param name="destination"> The destination state. </param>
        /// <returns> The transition. </returns>
        public AacFlTransition TransitionsTo(AacFlState destination)
        {
            return new AacFlTransition(
                ConfigureTransition(State.AddTransition(destination.State)),
                _machine,
                State,
                destination.State
            );
        }

        /// <summary> Create a transition from the any state to this state. </summary>
        /// <returns> The transition. </returns>
        public AacFlTransition TransitionsFromAny()
        {
            return new AacFlTransition(
                ConfigureTransition(_machine.AddAnyStateTransition(State)),
                _machine,
                null,
                State
            );
        }

        /// <summary> Create a transition from the entry state to this state. </summary>
        /// <returns> The transition. </returns>
        public AacFlEntryTransition TransitionsFromEntry()
        {
            return new AacFlEntryTransition(
                _machine.AddEntryTransition(State),
                _machine,
                null,
                State
            );
        }

        /// <summary> Create a transition with no exit time to the destination state. </summary>
        /// <param name="destination"> The destination state. </param>
        /// <returns> This instance. </returns>
        public AacFlState AutomaticallyMovesTo(AacFlState destination)
        {
            var transition = ConfigureTransition(State.AddTransition(destination.State));
            transition.hasExitTime = true;
            return this;
        }

        /// <summary> Create a transition with to the exit state. </summary>
        /// <returns> This instance. </returns>
        public AacFlTransition Exits()
        {
            return new AacFlTransition(
                ConfigureTransition(State.AddExitTransition()),
                _machine,
                State,
                null
            );
        }

        private AnimatorStateTransition ConfigureTransition(AnimatorStateTransition transition)
        {
            _defaultsProvider.ConfigureTransition(transition);
            return transition;
        }

#if VRC_SDK_VRCSDK3_AVATARS
        /// <summary> Drives the parameter provided to the value provided. </summary>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="value"> The value. </param>
        /// <returns> This instance. </returns>
        public AacFlState Drives(AacFlIntParameter parameter, int value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Set,
                    name = parameter.Name,
                    value = value
                }
            );
            return this;
        }

        /// <inheritdoc cref="Drives(AacFlIntParameter,int)"/>
        public AacFlState Drives(AacFlFloatParameter parameter, float value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Set,
                    name = parameter.Name,
                    value = value
                }
            );
            return this;
        }

        /// <summary> Adds the value provided to the parameter provided. </summary>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="additiveValue"> The additive value. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingIncreases(AacFlFloatParameter parameter, float additiveValue)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Add,
                    name = parameter.Name,
                    value = additiveValue
                }
            );
            return this;
        }

        /// <summary> Subtracts the value provided from the parameter provided. </summary>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="positiveValueToDecreaseBy"> The positive value to decrease by. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingDecreases(
            AacFlFloatParameter parameter,
            float positiveValueToDecreaseBy
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Add,
                    name = parameter.Name,
                    value = -positiveValueToDecreaseBy
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingIncreases(AacFlFloatParameter,float)"/>
        public AacFlState DrivingIncreases(AacFlIntParameter parameter, int additiveValue)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Add,
                    name = parameter.Name,
                    value = additiveValue
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingDecreases(AacFlFloatParameter,float)"/>
        public AacFlState DrivingDecreases(
            AacFlIntParameter parameter,
            int positiveValueToDecreaseBy
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Add,
                    name = parameter.Name,
                    value = -positiveValueToDecreaseBy
                }
            );
            return this;
        }

        /// <summary> Randomizes the parameter provided between the values provided. </summary>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingRandomizes(AacFlIntParameter parameter, int min, int max)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Random,
                    name = parameter.Name,
                    valueMin = min,
                    valueMax = max
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRandomizes(AacFlIntParameter,int,int)"/>
        /// <remarks> This version only applys to the local client </remarks>
        public AacFlState DrivingRandomizesLocally(
            AacFlFloatParameter parameter,
            float min,
            float max
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Random,
                    name = parameter.Name,
                    valueMin = min,
                    valueMax = max
                }
            );
            _driver.localOnly = true;
            return this;
        }

        /// <summary> Randomizes the parameter to true based on the chance provided. </summary>
        /// <inheritdoc cref="DrivingRandomizesLocally(AacFlFloatParameter,float,float)" path="/remarks"/>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="chance"> The chance. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingRandomizesLocally(AacFlBoolParameter parameter, float chance)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Random,
                    name = parameter.Name,
                    chance = chance
                }
            );
            _driver.localOnly = true;
            return this;
        }

        /// <inheritdoc cref="DrivingRandomizesLocally(AacFlFloatParameter, float, float)"/>
        public AacFlState DrivingRandomizesLocally(AacFlIntParameter parameter, int min, int max)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Random,
                    name = parameter.Name,
                    valueMin = min,
                    valueMax = max
                }
            );
            _driver.localOnly = true;
            return this;
        }

        /// <inheritdoc cref="Drives(AacFlFloatParameter,float)"/>
        public AacFlState Drives(AacFlBoolParameter parameter, bool value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    name = parameter.Name,
                    value = value ? 1 : 0
                }
            );
            return this;
        }

        /// <inheritdoc cref="Drives(AacFlFloatParameter,float)"/>
        public AacFlState Drives(AacFlBoolParameterGroup parameters, bool value)
        {
            CreateDriverBehaviorIfNotExists();
            foreach (var parameter in parameters.ToList())
            {
                _driver.parameters.Add(
                    new VRC_AvatarParameterDriver.Parameter
                    {
                        name = parameter.Name,
                        value = value ? 1 : 0
                    }
                );
            }
            return this;
        }

        /// <summary> Sets the parameter driver to local only. </summary>
        /// <returns> This instance. </returns>
        public AacFlState DrivingLocally()
        {
            CreateDriverBehaviorIfNotExists();
            _driver.localOnly = true;
            return this;
        }

        /// <summary> Drive the destination parameter to the source parameter. </summary>
        /// <param name="source"> The source parameter. </param>
        /// <param name="destination"> The destination parameter. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingCopies(AacFlIntParameter source, AacFlIntParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCopies(AacFlIntParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCopies(AacFlBoolParameter source, AacFlBoolParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCopies(AacFlIntParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCopies(AacFlFloatParameter source, AacFlFloatParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <summary> Drive the destination parameter to the source parameter. </summary>
        /// <remarks> Remaps the two parameters. </remarks>
        /// <param name="source"> The source parameter. </param>
        /// <param name="sourceMin"> The source minimum. </param>
        /// <param name="sourceMax"> The source maximum. </param>
        /// <param name="destination"> The destination parameter. </param>
        /// <param name="destinationMin"> The destination minimum. </param>
        /// <param name="destinationMax"> The destination maximum. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingRemaps(
            AacFlIntParameter source,
            int sourceMin,
            int sourceMax,
            AacFlIntParameter destination,
            int destinationMin,
            int destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingRemaps(
            AacFlBoolParameter source,
            float sourceMin,
            float sourceMax,
            AacFlBoolParameter destination,
            float destinationMin,
            float destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <summary> Converts the source parameter to the destination parameter type. </summary>
        /// <param name="source"> The source parameter. </param>
        /// <param name="destination"> The destination parameter. </param>
        /// <returns> This instance. </returns>
        public AacFlState DrivingCasts(AacFlFloatParameter source, AacFlIntParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCasts(AacFlFloatParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCasts(AacFlIntParameter source, AacFlFloatParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCasts(AacFlFloatParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCasts(AacFlBoolParameter source, AacFlFloatParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCasts(AacFlFloatParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCasts(AacFlFloatParameter source, AacFlBoolParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCasts(AacFlFloatParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCasts(AacFlBoolParameter source, AacFlIntParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingCasts(AacFlFloatParameter,AacFlIntParameter)"/>
        public AacFlState DrivingCasts(AacFlIntParameter source, AacFlBoolParameter destination)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = false
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingCasts(
            AacFlBoolParameter source,
            float sourceMin,
            float sourceMax,
            AacFlFloatParameter destination,
            float destinationMin,
            float destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingCasts(
            AacFlFloatParameter source,
            float sourceMin,
            float sourceMax,
            AacFlBoolParameter destination,
            float destinationMin,
            float destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingCasts(
            AacFlBoolParameter source,
            float sourceMin,
            float sourceMax,
            AacFlIntParameter destination,
            int destinationMin,
            int destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingCasts(
            AacFlIntParameter source,
            int sourceMin,
            int sourceMax,
            AacFlBoolParameter destination,
            float destinationMin,
            float destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        /// <inheritdoc cref="DrivingRemaps(AacFlIntParameter,int,int,AacFlIntParameter,int,int)"/>
        public AacFlState DrivingCasts(
            AacFlIntParameter source,
            int sourceMin,
            int sourceMax,
            AacFlFloatParameter destination,
            float destinationMin,
            float destinationMax
        )
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(
                new VRC_AvatarParameterDriver.Parameter
                {
                    type = VRC_AvatarParameterDriver.ChangeType.Copy,
                    source = source.Name,
                    name = destination.Name,
                    convertRange = true,
                    sourceMin = sourceMin,
                    sourceMax = sourceMax,
                    destMin = destinationMin,
                    destMax = destinationMax
                }
            );
            return this;
        }

        private void CreateDriverBehaviorIfNotExists()
        {
            if (_driver != null)
                return;
            _driver = State.AddStateMachineBehaviour<VRCAvatarParameterDriver>();
            _driver.parameters = new List<VRC_AvatarParameterDriver.Parameter>();
        }
#endif

        /// <summary> Sets the state's write defaults toggle to the given value. </summary>
        /// <param name="shouldWriteDefaults"> Should Write Defaults be on. </param>
        /// <returns> The current state. </returns>
        public AacFlState WithWriteDefaultsSetTo(bool shouldWriteDefaults)
        {
            State.writeDefaultValues = shouldWriteDefaults;
            return this;
        }

#if VRC_SDK_VRCSDK3_AVATARS
        /// <summary> Use an Animator Tracking Control to print logs to the avatar wearer. </summary>
        /// <param name="value"> The value to print. </param>
        /// <returns> The current state. </returns>
        public AacFlState PrintsToLogUsingTrackingBehaviour(string value)
        {
            CreateTrackingBehaviorIfNotExists();
            _tracking.debugString = value;

            return this;
        }

        /// <summary> Set an element to be tracking. </summary>
        /// <param name="element"> The element to track. </param>
        /// <returns> The current state. </returns>
        public AacFlState TrackingTracks(TrackingElement element)
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, VRC_AnimatorTrackingControl.TrackingType.Tracking);

            return this;
        }

        /// <summary> Set an element to be animated. </summary>
        /// <param name="element"> The element to animate. </param>
        /// <returns> The current state. </returns>
        public AacFlState TrackingAnimates(TrackingElement element)
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, VRC_AnimatorTrackingControl.TrackingType.Animation);

            return this;
        }

        public AacFlState TrackingSets(
            TrackingElement element,
            VRC_AnimatorTrackingControl.TrackingType trackingType
        )
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, trackingType);

            return this;
        }

        /// <summary> Enable locomotion. </summary>
        /// <returns> The current state. </returns>
        public AacFlState LocomotionEnabled()
        {
            CreateLocomotionBehaviorIfNotExists();
            _locomotionControl.disableLocomotion = false;

            return this;
        }

        /// <summary> Disable locomotion. </summary>
        /// <returns> The current state. </returns>
        public AacFlState LocomotionDisabled()
        {
            CreateLocomotionBehaviorIfNotExists();
            _locomotionControl.disableLocomotion = true;

            return this;
        }

        private void SettingElementTo(
            TrackingElement element,
            VRC_AnimatorTrackingControl.TrackingType target
        )
        {
            switch (element)
            {
                case TrackingElement.Head:
                    _tracking.trackingHead = target;
                    break;
                case TrackingElement.LeftHand:
                    _tracking.trackingLeftHand = target;
                    break;
                case TrackingElement.RightHand:
                    _tracking.trackingRightHand = target;
                    break;
                case TrackingElement.Hip:
                    _tracking.trackingHip = target;
                    break;
                case TrackingElement.LeftFoot:
                    _tracking.trackingLeftFoot = target;
                    break;
                case TrackingElement.RightFoot:
                    _tracking.trackingRightFoot = target;
                    break;
                case TrackingElement.LeftFingers:
                    _tracking.trackingLeftFingers = target;
                    break;
                case TrackingElement.RightFingers:
                    _tracking.trackingRightFingers = target;
                    break;
                case TrackingElement.Eyes:
                    _tracking.trackingEyes = target;
                    break;
                case TrackingElement.Mouth:
                    _tracking.trackingMouth = target;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }

        private void CreateTrackingBehaviorIfNotExists()
        {
            if (_tracking != null)
                return;
            _tracking = State.AddStateMachineBehaviour<VRCAnimatorTrackingControl>();
        }

        private void CreateLocomotionBehaviorIfNotExists()
        {
            if (_locomotionControl != null)
                return;
            _locomotionControl = State.AddStateMachineBehaviour<VRCAnimatorLocomotionControl>();
        }

        public enum TrackingElement
        {
            Head,
            LeftHand,
            RightHand,
            Hip,
            LeftFoot,
            RightFoot,
            LeftFingers,
            RightFingers,
            Eyes,
            Mouth
        }
#endif

        /// <summary> Set the Motion Time, formerly known as Normalized Time. </summary>
        /// <param name="floatParam"> The float parameter to use. </param>
        /// <returns> The current state. </returns>
        public AacFlState MotionTime(AacFlFloatParameter floatParam)
        {
            State.timeParameterActive = true;
            State.timeParameter = floatParam.Name;

            return this;
        }

        /// <summary> Set the speed of the animation. </summary>
        /// <param name="parameter"> The float parameter to use. </param>
        /// <returns> The current state. </returns>
        public AacFlState WithSpeed(AacFlFloatParameter parameter)
        {
            State.speedParameter = parameter.Name;
            State.speedParameterActive = true;

            return this;
        }
    }

    public class AacFlTransition : AacFlNewTransitionContinuation
    {
        private static readonly PropertyInfo PropPushUndo =
            typeof(AnimatorTransitionBase).GetProperty(
                "pushUndo",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
        private readonly AnimatorStateTransition _transition;

        public AacFlTransition(
            AnimatorStateTransition transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
            : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
            _transition = transition;
            PropPushUndo.SetValue(_transition, false);
        }

        public AacFlTransition WithSourceInterruption()
        {
            _transition.interruptionSource = TransitionInterruptionSource.Source;
            return this;
        }

        public AacFlTransition WithTransitionDurationSeconds(float transitionDuration)
        {
            _transition.duration = transitionDuration;
            return this;
        }

        public AacFlTransition WithOrderedInterruption()
        {
            _transition.orderedInterruption = true;
            return this;
        }

        public AacFlTransition WithNoOrderedInterruption()
        {
            _transition.orderedInterruption = false;
            return this;
        }

        public AacFlTransition WithTransitionToSelf()
        {
            _transition.canTransitionToSelf = true;
            return this;
        }

        public AacFlTransition WithNoTransitionToSelf()
        {
            _transition.canTransitionToSelf = false;
            return this;
        }

        public AacFlTransition AfterAnimationFinishes()
        {
            _transition.hasExitTime = true;
            _transition.exitTime = 1;

            return this;
        }

        public AacFlTransition AfterAnimationIsAtLeastAtPercent(float exitTimeNormalized)
        {
            _transition.hasExitTime = true;
            _transition.exitTime = exitTimeNormalized;

            return this;
        }

        public AacFlTransition WithTransitionDurationPercent(float transitionDurationNormalized)
        {
            _transition.hasFixedDuration = false;
            _transition.duration = transitionDurationNormalized;

            return this;
        }
    }

    public class AacFlEntryTransition : AacFlNewTransitionContinuation
    {
        public AacFlEntryTransition(
            AnimatorTransition transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
            : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits) { }
    }

    public interface IAacFlCondition
    {
        void ApplyTo(AacFlCondition appender);
    }

    public interface IAacFlOrCondition
    {
        List<AacFlTransitionContinuation> ApplyTo(AacFlNewTransitionContinuation firstContinuation);
    }

    public class AacFlCondition
    {
        private static readonly PropertyInfo PropPushUndo =
            typeof(AnimatorTransitionBase).GetProperty(
                "pushUndo",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
        private readonly AnimatorTransitionBase _transition;

        public AacFlCondition(AnimatorTransitionBase transition)
        {
            _transition = transition;
            PropPushUndo.SetValue(_transition, false);
        }

        public AacFlCondition Add(string parameter, AnimatorConditionMode mode, float threshold)
        {
            _transition.AddCondition(mode, threshold, parameter);
            return this;
        }
    }

    public class AacFlNewTransitionContinuation
    {
        public readonly AnimatorTransitionBase Transition;
        private readonly AnimatorStateMachine _machine;
        private readonly AnimatorState _sourceNullableIfAny;
        private readonly AnimatorState _destinationNullableIfExits;

        public AacFlNewTransitionContinuation(
            AnimatorTransitionBase transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
        {
            Transition = transition;
            _machine = machine;
            _sourceNullableIfAny = sourceNullableIfAny;
            _destinationNullableIfExits = destinationNullableIfExits;
        }

        /// Adds a condition to the transition.
        ///
        /// The settings of the transition can no longer be modified after this point.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(AacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(AacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public AacFlTransitionContinuation When(IAacFlCondition action)
        {
            action.ApplyTo(new AacFlCondition(Transition));
            return AsContinuationWithOr();
        }

        /// <summary>
        /// Applies a series of conditions to this transition, but this series of conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public AacFlTransitionContinuation When(
            Action<AacFlTransitionContinuationWithoutOr> actionsWithoutOr
        )
        {
            actionsWithoutOr(new AacFlTransitionContinuationWithoutOr(Transition));
            return AsContinuationWithOr();
        }

        /// <summary>
        /// Applies a series of conditions, and this series may contain Or operators. However, the result can not be followed by an And operator. It can only be an Or operator.
        /// </summary>
        /// <param name="actionsWithOr"></param>
        /// <returns></returns>
        public AacFlTransitionContinuationOnlyOr When(
            Action<AacFlNewTransitionContinuation> actionsWithOr
        )
        {
            actionsWithOr(this);
            return AsContinuationOnlyOr();
        }

        /// <summary>
        /// Applies a series of conditions, and this series may contain Or operators. All And operators that follow will apply to all the conditions generated by this series, until the next Or operator.
        /// </summary>
        /// <param name="actionsWithOr"></param>
        /// <returns></returns>
        public AacFlMultiTransitionContinuation When(IAacFlOrCondition actionsWithOr)
        {
            var pendingContinuations = actionsWithOr.ApplyTo(this);
            return new AacFlMultiTransitionContinuation(
                Transition,
                _machine,
                _sourceNullableIfAny,
                _destinationNullableIfExits,
                pendingContinuations
            );
        }

        public AacFlTransitionContinuation WhenConditions()
        {
            return AsContinuationWithOr();
        }

        private AacFlTransitionContinuation AsContinuationWithOr()
        {
            return new AacFlTransitionContinuation(
                Transition,
                _machine,
                _sourceNullableIfAny,
                _destinationNullableIfExits
            );
        }

        private AacFlTransitionContinuationOnlyOr AsContinuationOnlyOr()
        {
            return new AacFlTransitionContinuationOnlyOr(
                Transition,
                _machine,
                _sourceNullableIfAny,
                _destinationNullableIfExits
            );
        }
    }

    public class AacFlTransitionContinuation : AacFlTransitionContinuationAbstractWithOr
    {
        public AacFlTransitionContinuation(
            AnimatorTransitionBase transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
            : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits) { }

        /// Adds an additional condition to the transition that requires all preceding conditions to be true.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(AacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(AacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public AacFlTransitionContinuation And(IAacFlCondition action)
        {
            action.ApplyTo(new AacFlCondition(Transition));
            return this;
        }

        /// <summary>
        /// Applies a series of conditions to this transition. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public AacFlTransitionContinuation And(
            Action<AacFlTransitionContinuationWithoutOr> actionsWithoutOr
        )
        {
            actionsWithoutOr(new AacFlTransitionContinuationWithoutOr(Transition));
            return this;
        }
    }

    public class AacFlMultiTransitionContinuation : AacFlTransitionContinuationAbstractWithOr
    {
        private readonly List<AacFlTransitionContinuation> _pendingContinuations;

        public AacFlMultiTransitionContinuation(
            AnimatorTransitionBase transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits,
            List<AacFlTransitionContinuation> pendingContinuations
        )
            : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
            _pendingContinuations = pendingContinuations;
        }

        /// Adds an additional condition to these transitions that requires all preceding conditions to be true.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(AacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(AacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public AacFlMultiTransitionContinuation And(IAacFlCondition action)
        {
            foreach (var pendingContinuation in _pendingContinuations)
            {
                pendingContinuation.And(action);
            }

            return this;
        }

        /// <summary>
        /// Applies a series of conditions to these transitions. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public AacFlMultiTransitionContinuation And(
            Action<AacFlTransitionContinuationWithoutOr> actionsWithoutOr
        )
        {
            foreach (var pendingContinuation in _pendingContinuations)
            {
                pendingContinuation.And(actionsWithoutOr);
            }

            return this;
        }
    }

    public class AacFlTransitionContinuationOnlyOr : AacFlTransitionContinuationAbstractWithOr
    {
        public AacFlTransitionContinuationOnlyOr(
            AnimatorTransitionBase transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
            : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits) { }
    }

    public abstract class AacFlTransitionContinuationAbstractWithOr
    {
        protected readonly AnimatorTransitionBase Transition;
        private readonly AnimatorStateMachine _machine;
        private readonly AnimatorState _sourceNullableIfAny;
        private readonly AnimatorState _destinationNullableIfExits;

        public AacFlTransitionContinuationAbstractWithOr(
            AnimatorTransitionBase transition,
            AnimatorStateMachine machine,
            AnimatorState sourceNullableIfAny,
            AnimatorState destinationNullableIfExits
        )
        {
            Transition = transition;
            _machine = machine;
            _sourceNullableIfAny = sourceNullableIfAny;
            _destinationNullableIfExits = destinationNullableIfExits;
        }

        /// <summary>
        /// Creates a new transition with identical settings but having no conditions defined yet.
        /// </summary>
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(AacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(AacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public AacFlNewTransitionContinuation Or()
        {
            return new AacFlNewTransitionContinuation(
                NewTransitionFromTemplate(),
                _machine,
                _sourceNullableIfAny,
                _destinationNullableIfExits
            );
        }

        private AnimatorTransitionBase NewTransitionFromTemplate()
        {
            AnimatorTransitionBase newTransition;
            if (Transition is AnimatorStateTransition templateStateTransition)
            {
                var stateTransition = NewTransition();
                stateTransition.duration = templateStateTransition.duration;
                stateTransition.offset = templateStateTransition.offset;
                stateTransition.interruptionSource = templateStateTransition.interruptionSource;
                stateTransition.orderedInterruption = templateStateTransition.orderedInterruption;
                stateTransition.exitTime = templateStateTransition.exitTime;
                stateTransition.hasExitTime = templateStateTransition.hasExitTime;
                stateTransition.hasFixedDuration = templateStateTransition.hasFixedDuration;
                stateTransition.canTransitionToSelf = templateStateTransition.canTransitionToSelf;
                newTransition = stateTransition;
            }
            else
            {
                newTransition = _machine.AddEntryTransition(_destinationNullableIfExits);
            }

            return newTransition;
        }

        private AnimatorStateTransition NewTransition()
        {
            if (_sourceNullableIfAny == null)
            {
                return _machine.AddAnyStateTransition(_destinationNullableIfExits);
            }

            if (_destinationNullableIfExits == null)
            {
                return _sourceNullableIfAny.AddExitTransition();
            }

            return _sourceNullableIfAny.AddTransition(_destinationNullableIfExits);
        }
    }

    public class AacFlTransitionContinuationWithoutOr
    {
        private readonly AnimatorTransitionBase _transition;

        public AacFlTransitionContinuationWithoutOr(AnimatorTransitionBase transition)
        {
            _transition = transition;
        }

        public AacFlTransitionContinuationWithoutOr And(IAacFlCondition action)
        {
            action.ApplyTo(new AacFlCondition(_transition));
            return this;
        }

        /// <summary>
        /// Applies a series of conditions to this transition. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public AacFlTransitionContinuationWithoutOr AndWhenever(
            Action<AacFlTransitionContinuationWithoutOr> action
        )
        {
            action(this);
            return this;
        }
    }
}
