using System;
using UnityEngine;
using static ChairMicroControllerState;
using static MessageHelper;

namespace Messaging
{
    public static class ChairMessageParser
    {
        public static RawChairStatus ParseMessage(string msg)
        {
            try
            {
                return JsonUtility.FromJson<RawChairStatus>(msg);
            }
            catch(ArgumentException e)
            {
                Debug.LogError($"JSON error for {msg} has exception: {e.Message}\n {e.StackTrace}");
            }

            return null;
        }

        public static void UpdateChairState(RawChairStatus raw, ChairMicroControllerState state)
        {
            state.time_since_started = raw.time_since_started;

            state.chair_position_estimated = ConvertPosition(raw.chair_position_estimated, state);
            state.chair_position_target = ConvertPosition(raw.chair_position_target, state);
            state.chair_position_motor_direction = ConvertDirection(raw.chair_position_motor_direction);
            state.chair_position_move_time_max = ConvertMsToSec(raw.chair_position_move_time_max);
            state.chair_position_move_time_up = ConvertMsToSec(raw.chair_position_move_time_up);
            state.chair_position_move_time_down = ConvertMsToSec(raw.chair_position_move_time_down);

            state.roller_position_estimated = ConvertPosition(raw.roller_position_estimated, state);
            state.roller_position_target = ConvertPosition(raw.roller_position_target, state);
            state.roller_position_motor_direction = ConvertDirection(raw.roller_position_motor_direction);
            state.roller_move_time_up = ConvertMsToSec(raw.roller_move_time_up);
            state.roller_move_time_down = ConvertMsToSec(raw.roller_move_time_down);
            state.roller_sensor_top = ConvertToBool(raw.roller_sensor_top);
            state.roller_sensor_bottom = ConvertToBool(raw.roller_sensor_bottom);

            state.roller_kneading_on = ConvertToBool(raw.roller_kneading_on);
            state.roller_kneading_speed = ConvertSpeed(raw.roller_kneading_speed, state);

            state.roller_pounding_on = ConvertToBool(raw.roller_pounding_on);
            state.roller_pounding_speed = ConvertSpeed(raw.roller_pounding_speed, state);

            state.feet_roller_on = ConvertToBool(raw.feet_roller_on);
            state.feet_roller_speed = ConvertSpeed(raw.feet_roller_speed, state);

            state.airpump_on = ConvertToBool(raw.airpump_on);
            state.airbag_time_max = ConvertMsToSec(raw.airbag_time_max);
            state.airbag_shoulders_on = ConvertToBool(raw.airbag_shoulders_on);
            state.airbag_arms_on = ConvertToBool(raw.airbag_arms_on);
            state.airbag_legs_on = ConvertToBool(raw.airbag_legs_on);
            state.airbag_outside_on = ConvertToBool(raw.airbag_outside_on);

            state.butt_vibration_on = ConvertToBool(raw.butt_vibration_on);
            state.backlight_on = ConvertToBool(raw.backlight_on);
            state.backlight_color = ConvertToColor(raw.backlight_color);

            state.redgreen_statuslight = ConvertToRedGreen(raw.redgreen_statuslight);
            state.button_bounce_time = ConvertMsToSec(raw.button_bounce_time);
        }

        private static StatusLight ConvertToRedGreen(int value)
        {
            return value == 0 ? StatusLight.Green : StatusLight.Red;
        }

        public static int ConvertFromRedGreen(StatusLight value)
        {
            return value == StatusLight.Green ? 0 : 1;
        }

        private static float ConvertSpeed(int value, ChairMicroControllerState state)
        {
            return (float) value / (float) state.MaxSpeed;
        }

        private static float ConvertPosition(int position, ChairMicroControllerState state)
        {
            return (float) position / (float) state.MaxPosition;
        }

        private static ChairMotorDirection ConvertDirection(int dir)
        {
            switch (dir)
            {
                case 1:
                    return ChairMotorDirection.Up;
                case -1:
                    return ChairMotorDirection.Down;
                default:
                    return ChairMotorDirection.Neutral;
            }
        }
    }
}