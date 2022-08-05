using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public abstract class PickUp : MonoBehaviour
{
    protected abstract void OnPickup(PlayerMovement player);

    [Header("Feedback")]
    [SerializeField] AudioClip _pickupSFX = null;
    [SerializeField] ParticleSystem _particlePrefab = null;

    Collider _collider = null;
    AudioSource _audioSource = null;
    public bool rotate = true;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (rotate)
            transform.Rotate(Vector3.up * 200 * Time.deltaTime, Space.World);

    }

    // Reset gets called whenever you add a component to an object
    private void Reset()
    {
        // set isTrigger in the Inspector, just in case Designer forgot
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // guard clause
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null)
            return;

        // found the player! call our abstract method and supporting feedback
        OnPickup(player);

        if (_pickupSFX != null)
        {
            SpawnAudio(_pickupSFX);
        }

        if (_particlePrefab != null)
        {
            SpawnParticle(_particlePrefab);
        }

        Disable();
    }

    void SpawnAudio(AudioClip pickupSFX)
    {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
    }

    void SpawnParticle(ParticleSystem pickupParticles)
    {
        ParticleSystem newParticles =
            Instantiate(pickupParticles, transform.position, Quaternion.identity);
        newParticles.Play();
    }

    // allow override in case subclass wants to object pool, etc.
    protected virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
