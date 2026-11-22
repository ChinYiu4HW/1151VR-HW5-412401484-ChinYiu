using UnityEngine;

public class PokeBallController : MonoBehaviour
{
    [Header("VFX")]
    public GameObject smokeVfxPrefab1;
    public GameObject smokeVfxPrefab2;
    public float vfxDuration = 3f;

    [Header("Sound")]
    public AudioClip hitSound;
    [Range(0f, 1f)]
    public float soundVolume = 1f;
    [Tooltip("第幾秒開始播放")]
    public float audioStartOffset = 0f;

    private AudioSource sfxSource;

    void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        sfxSource.playOnAwake = false;
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("pkm"))
        {
            Vector3 hitPos = other.contacts.Length > 0
                ? other.contacts[0].point
                : other.transform.position;

            SpawnVfx(smokeVfxPrefab1, hitPos);
            SpawnVfx(smokeVfxPrefab2, hitPos);

            if (hitSound != null && sfxSource != null)
            {
                sfxSource.clip = hitSound;
                sfxSource.volume = soundVolume;
                sfxSource.time = Mathf.Clamp(audioStartOffset, 0f, Mathf.Max(0f, hitSound.length - 0.05f));
                sfxSource.Play();
            }

            Destroy(other.gameObject);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void SpawnVfx(GameObject prefab, Vector3 pos)
    {
        if (prefab == null) return;

        GameObject vfx = Instantiate(prefab, pos, Quaternion.identity);
        vfx.SetActive(true);

        ParticleSystem[] systems = vfx.GetComponentsInChildren<ParticleSystem>(true);
        foreach (ParticleSystem ps in systems)
        {
            ps.gameObject.SetActive(true);
            ps.Clear();
            ps.Play(true);
        }

        Destroy(vfx, vfxDuration);
    }
}