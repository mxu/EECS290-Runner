using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour {

	private const float
		hrToDeg = 360f / 12f,
		minToDeg = 360f / 60f,
		secToDeg = 360f / 60f,
		msToDeg = 360f / 1000f;

	public Transform hours, minutes, seconds, milliseconds;
	public bool analog;

	void Update() {
		float hr, min, sec, ms;

		if(analog) {
			TimeSpan timespan = DateTime.Now.TimeOfDay;
			hr = (float)timespan.TotalHours;
			min = (float)timespan.TotalMinutes;
			sec = (float)timespan.TotalSeconds;
			ms = (float)timespan.TotalMilliseconds;
		} else {
			DateTime time = DateTime.Now;
			hr = time.Hour;
			min = time.Minute;
			sec = time.Second;
			ms = Mathf.Floor(time.Millisecond / 100f) * 100f;
		}

		hours.localRotation = Quaternion.Euler (0f, 0f, hr * -hrToDeg);
		minutes.localRotation = Quaternion.Euler (0f, 0f, min * -minToDeg);
		seconds.localRotation = Quaternion.Euler (0f, 0f, sec * -secToDeg);
		milliseconds.localRotation = Quaternion.Euler (0f, 0f, ms * -msToDeg);
	}
}
