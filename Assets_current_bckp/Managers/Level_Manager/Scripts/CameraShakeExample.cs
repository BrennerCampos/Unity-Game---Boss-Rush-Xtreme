// Copyright (c) 2011-2017 Thinksquirrel Inc.

using UnityEngine;
using System.Collections;
using Thinksquirrel.CShake;

namespace Thinksquirrel.CShakeExample
{
    /*[AddComponentMenu("Camera Shake Example/Camera Shake Example")]
    [RequireComponent(typeof(CameraShake))]
    public class CameraShakeExample : MonoBehaviour
    {
        public bool is2DExample;
        public float rigidbodyForceModifier = 2.0f;
        public float rigidbodyTorqueModifier = 0.2f;

        #region Private variables
        private CanvasGroup m_Canvas;
        private CameraShake m_Shake;
        private bool m_ShakeEverything;
        private Rigidbody[] m_Rigidbodies;
        private Rigidbody2D[] m_Rigidbodies2D;
        private bool m_MultiShake;
        #endregion

        #region MonoBehaviour methods
        private void Start()
        {
            m_Canvas = FindObjectOfType<CanvasGroup>();
            m_Shake = GetComponent<CameraShake>();
            m_Shake.onShakeOffset += OnShakeOffset;
            m_Rigidbodies = FindObjectsOfType<Rigidbody>();
            m_Rigidbodies2D = FindObjectsOfType<Rigidbody2D>();
        }
        #endregion

        private void Update()
        {
            if (m_Canvas)
            {
                m_Canvas.interactable = !m_Shake.IsShaking() && !m_MultiShake;
            }

            // Parent camera motion
            if (m_Shake.shakeType == CameraShake.ShakeType.CameraMatrix)
            {
                transform.parent.position = new Vector3(
                    transform.parent.position.x,
                    Mathf.Cos(Time.time) * .35f,
                    transform.parent.position.z);

                transform.localPosition = new Vector3(
                    Mathf.Sin(Time.time) * .5f,
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
            else
            {
                transform.parent.position = new Vector3(
                    Mathf.Sin(Time.time) * .5f,
                    Mathf.Cos(Time.time) * .35f,
                    transform.parent.position.z);
            }
        }

        #region World shaking
        private void OnShakeOffset(Vector3 translation, Quaternion rotation)
        {
            if (!m_ShakeEverything) return;

            if (is2DExample)
            {
                foreach (var rb in m_Rigidbodies2D)
                {
                    if (!rb) continue;
                    rb.AddForce(translation * rigidbodyForceModifier, ForceMode2D.Impulse);
                    var rot1 = Quaternion.Euler(0, 0, transform.eulerAngles.z);
                    var rot2 = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
                    rb.AddTorque(Quaternion.Angle(rot1, rot2) * rigidbodyTorqueModifier, ForceMode2D.Impulse);
                }
            }
            else
            {
                foreach (var rb in m_Rigidbodies)
                {
                    if (!rb) continue;
                    rb.AddForce(translation * rigidbodyForceModifier, ForceMode.Impulse);
                    Vector3 delta;
                    var rot1 = Quaternion.Euler(transform.eulerAngles.x, 0, 0);
                    var rot2 = Quaternion.Euler(rotation.eulerAngles.x, 0, 0);
                    delta.x = Quaternion.Angle(rot1, rot2);
                    rot1 = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                    rot2 = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                    delta.y = Quaternion.Angle(rot1, rot2);
                    rot1 = Quaternion.Euler(0, 0, transform.eulerAngles.z);
                    rot2 = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
                    delta.z = Quaternion.Angle(rot1, rot2);
                    rb.AddTorque(delta * rigidbodyTorqueModifier, ForceMode.Impulse);
                }
            }
        }
        #endregion

        #region Preset shakes
        private void Explosion()
        {
            m_MultiShake = true;
            m_ShakeEverything = true;

            // Single shake
            var shake = Vector3.one;
            var rot = new Vector3(0.2f, 0.05f, 1.0f);

            if (is2DExample)
            {
                shake.z = 0;
                rot.x = 0;
                rot.y = 0;
            }

            m_Shake.Shake(m_Shake.shakeType,
                          5,
                          shake,
                          rot,
                          0.25f,
                          50.0f,
                          0.20f,
                          1.0f,
                          true,
                          () => m_MultiShake = false);
        }
        private void Footsteps()
        {
            m_ShakeEverything = true;
            m_MultiShake = true;

            // Sequential shakes

            StartCoroutine(DoFootsteps());
        }
        private IEnumerator DoFootsteps()
        {
            var shake = Vector3.one;
            var rot = new Vector3(0.2f, 0.05f, 0.1f);

            if (is2DExample)
            {
                shake.z = 0;
                rot.x = 0;
                rot.y = 0;
            }

            m_Shake.Shake(m_Shake.shakeType, 3, shake, rot, 0.02f, 50.0f, 0.50f, 1.0f, true, null);
            yield return new WaitForSeconds(1.0f);
            m_Shake.Shake(m_Shake.shakeType, 3, shake, rot, 0.03f, 50.0f, 0.50f, 1.0f, true, null);
            yield return new WaitForSeconds(1.0f);
            m_Shake.Shake(m_Shake.shakeType, 3, shake, rot * 1.5f, 0.04f, 50.0f, 0.50f, 1.0f, true, null);
            yield return new WaitForSeconds(1.0f);
            m_Shake.Shake(m_Shake.shakeType, 3, shake, rot * 2f, 0.05f, 50.0f, 0.50f, 1.0f, true, null);
            yield return new WaitForSeconds(1.0f);
            m_Shake.Shake(m_Shake.shakeType,
                          3,
                          shake,
                          rot * 2.5f,
                          0.06f,
                          50.0f,
                          0.50f,
                          1.0f,
                          true,
                          () => m_MultiShake = false);
        }
        private void Earthquake()
        {
            m_ShakeEverything = true;
            m_MultiShake = true;

            // Multiple sequential shakes at once

            StartCoroutine(DoEarthquake());
            StartCoroutine(DoEarthquake2());
        }
        private IEnumerator DoEarthquake()
        {
            var shake = Vector3.one;
            var rot = new Vector3(0.2f, 0.2f, 0.2f);

            if (is2DExample)
            {
                shake.z = 0;
                rot.x = 0;
                rot.y = 0;
            }

            for (var i = 0; i < 50; i++)
            {
                m_Shake.Shake(m_Shake.shakeType, 3, shake, rot, 0.02f, 50.0f, 0.00f, 1.0f, true, null);
                yield return new WaitForSeconds(0.1f);
            }
            m_Shake.Shake(m_Shake.shakeType,
                          3,
                          shake,
                          rot,
                          0.02f,
                          50.0f,
                          0.00f,
                          1.0f,
                          true,
                          () => m_MultiShake = false);
        }
        private IEnumerator DoEarthquake2()
        {
            var rot = new Vector3(0.8f, 0.1f, 0.4f);

            if (is2DExample)
            {
                rot.x = 0;
                rot.y = 0;
            }

            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(1.0f);
                m_Shake.Shake(m_Shake.shakeType, 10, Vector3.up, rot, 0.50f, 50.0f, 0.20f, 1.0f, true, null);
            }
        }
        #endregion

        #region UI callbacks
        public void OnShakeCamera()
        {
            m_ShakeEverything = false;
            m_Shake.Shake();
        }
        public void OnShakeEverything()
        {
            m_ShakeEverything = true;
            m_Shake.Shake();
        }
        public void OnNumberOfShakesSlider(float val)
        {
            m_Shake.numberOfShakes = (int) val;
        }
        public void OnShakeAmountXSlider(float val)
        {
            var shakeAmount = m_Shake.shakeAmount;
            shakeAmount.x = val;
            m_Shake.shakeAmount = shakeAmount;
        }
        public void OnShakeAmountYSlider(float val)
        {
            var shakeAmount = m_Shake.shakeAmount;
            shakeAmount.y = val;
            m_Shake.shakeAmount = shakeAmount;
        }
        public void OnShakeAmountZSlider(float val)
        {
            var shakeAmount = m_Shake.shakeAmount;
            shakeAmount.z = val;
            m_Shake.shakeAmount = shakeAmount;
        }
        public void OnRotationAmountXSlider(float val)
        {
            var rotationAmount = m_Shake.rotationAmount;
            rotationAmount.x = val;
            m_Shake.rotationAmount = rotationAmount;
        }
        public void OnRotationAmountYSlider(float val)
        {
            var rotationAmount = m_Shake.rotationAmount;
            rotationAmount.y = val;
            m_Shake.rotationAmount = rotationAmount;
        }
        public void OnRotationAmountZSlider(float val)
        {
            var rotationAmount = m_Shake.rotationAmount;
            rotationAmount.z = val;
            m_Shake.rotationAmount = rotationAmount;
        }
        public void OnDistanceSlider(float val)
        {
            m_Shake.distance = val;
        }
        public void OnSpeedSlider(float val)
        {
            m_Shake.distance = val;
        }
        public void OnDecaySlider(float val)
        {
            m_Shake.decay = val;
        }
        public void OnPresetExplosion()
        {
            Explosion();
        }
        public void OnPresetFootsteps()
        {
            Footsteps();
        }
        public void OnPresetEarthquake()
        {
            Earthquake();
        }
        #endregion
    }*/
}
