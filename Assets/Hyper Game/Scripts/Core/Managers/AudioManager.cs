using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource musicSource; // AudioSource để phát nhạc nền
    [SerializeField] private AudioClip backgroundMusic; // Nhạc nền mặc định

    [SerializeField] private AudioClip buttonClickSFX; // gán clip "bit bit" vào đây
    private AudioSource sfxSource; // AudioSource riêng để phát SFX

    private void Awake()
    {
        // Singleton: Giữ lại `AudioManager` khi chuyển Scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không hủy khi chuyển Scene
        }
        else
        {
            Destroy(gameObject); // Nếu đã có `AudioManager`, hủy bản sao mới
            return;
        }

        // Nếu chưa gán `AudioSource`, tự động lấy từ GameObject
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        // Cấu hình AudioSource
        musicSource.loop = true; // Nhạc nền chạy liên tục
        musicSource.playOnAwake = false; // Không tự động phát khi Scene load
        musicSource.volume = 0.2f; // Đặt âm lượng mặc định

        // Phát nhạc nền mặc định nếu có
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;
    }

    // Hàm phát nhạc nền mới
    public void PlayMusic(AudioClip newMusic)
    {
        if (musicSource.clip == newMusic) return; // Nếu đang phát nhạc này, bỏ qua

        musicSource.clip = newMusic;
        musicSource.Play();
    }

    // Hàm thay đổi âm lượng nhạc nền
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    // Hàm dừng nhạc
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Phát tiếng "bit bit" mặc định khi bấm nút
    public void PlayButtonClick()
    {
        Debug.Log("check click");
        PlaySFX(buttonClickSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    
}
