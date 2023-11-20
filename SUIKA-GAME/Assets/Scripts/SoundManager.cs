using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name; // ���� �̸��� ���ڿ��� �ν����� â���� ����
    public AudioClip clip; // ���� Ŭ���� ����� Ŭ������ �ν����� â���� ����
}

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Play; // �� Ŭ���� ��ü�� �ν��Ͻ�ȭ ���� �ٸ� Ŭ���������� ȣ�� �����ϰ� �Ѵ�

    public AudioSource[] effect; // ȿ���� ����� �ҽ��� �迭 ���� ���̰� ȿ������ ���� ���� �����Ѵ�
    public AudioSource background; // ����� ����� �ҽ� ���� ��

    public string[] playSoundName; // ���� �̸��� ������ ���ڿ� �迭 ���� ��

    public Sound[] effectSound; // ȿ���� ���� �ҽ��� ������ �迭 ������ �� Ŭ���� ����� name, clip�� ������ ������
    public Sound backgroundSound; // ����� ���� �ҽ��� ������ ������ �� Ŭ���� ����� name, clip�� ������ ������

    public static bool isSoundChecked; // ���� üũ�� ���� �� or ���� �Ҹ��� ��


    private void Awake() // ���� ���ʿ� �� �� ����Ǵ� �޼ҵ�
    {
        OnSoundManagerInit(); // ���� �Ŵ��� ���� �޼ҵ�
    }

    private void OnSoundManagerInit() // ���� �Ŵ��� ���� �޼ҵ�
    {
        if (Play == null) // �ν��Ͻ�ȭ�� �ȵǾ��ִٸ�
        {
            Play = this; // �� Ŭ������ �ν��Ͻ��� �����Ѵ�
            DontDestroyOnLoad(gameObject); // ���� ������Ʈ�� ���� ������ �̵��Ͽ��� �ı��� �� ���� �����Ѵ�
        }
        else // ���� ���ǿ� �������� �ʴٸ�
        {
            Destroy(gameObject); // ���� ������Ʈ�� �ı��Ѵ�, �̴� �ٽ� Ÿ��Ʋ ȭ������ ���ư��� ��� ���� �����Ǵ� ���� �Ŵ����� �����ϱ� �����̴�
        }

        if (isSoundChecked == false) // ���� üũ�� ���� �� or ���� �Ҹ��� ���� �����̶��
        {
            PlayEffect("PlanetOut"); // ���� �̸����� ���� ���
            isSoundChecked = true; // ���� üũ�� ���� �� or ���� �Ҹ��� ���� ������ �����Ѵ�
        }
        
        playSoundName = new string[effectSound.Length]; // ���� ���� ���� �̸��� effectSound ���̷� �Ѵ�
        OnVolumeChange(); // ���� ũ�⸦ �����ϴ� �޼ҵ�
    }

    private void Start() // ��ŸƮ �޼ҵ�
    {
        PlayBackground("Background"); // ����� ���� �޼ҵ�
    }

    private void Update() // ������Ʈ �޼ҵ�
    {
        OnVolumeChange(); // ���� ũ�⸦ �����ϴ� �޼ҵ�
    }

    private void OnVolumeChange() // ���� ũ�⸦ �����ϴ� �޼ҵ�
    {
        background.volume = PlayerPrefs.GetFloat("BackgroundVolume"); // ����� ũ�⸦ �����ߴ� ������ �����Ѵ�

        for (int i = 0; i < effect.Length; ++i) // ȿ���� ũ�⸦ ������ �迭�� ũ�⸸ŭ �ݺ����� �����Ѵ�
        {
            effect[i].volume = PlayerPrefs.GetFloat("EffectVolume"); // ȿ���� ũ�⸦ �����ߴ� ������ �����Ѵ�
        }
    }

    public void PlayEffect(string _soundName) // ȿ���� ���� �޼ҵ�
    {
        if (_soundName == null) // ���� �̸��� �������� �ʴٸ�
        {
            return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
        }

        for (int i = 0; i < effectSound.Length; ++i) // ȿ���� ũ�⸦ ������ �迭�� ũ�⸸ŭ �ݺ����� �����Ѵ�
        {
            if (_soundName.Equals(effectSound[i].name) == true) // ���� ������ ���� ���� �̸��� ȿ���� ���� �̸��� ���ٸ�
            {
                for (int j = 0; j < effect.Length; ++j) // ȿ���� ���� ũ�⸦ ������ �迭�� ũ�⸸ŭ �ݺ����� �����Ѵ�
                {
                    if (effect[j].isPlaying == false) // ȿ������ ���������� �ʴٸ�
                    {
                        playSoundName[j] = effectSound[i].name; // ���� ���� ���� �̸��� ȿ���� ���� �̸����� �����Ѵ�
                        effect[j].clip = effectSound[i].clip; // ���� Ŭ���� ȿ���� ���� Ŭ������ �����Ѵ�
                        effect[j].Play(); // ���� Ŭ���� �����Ѵ�

                        return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
                    }
                }

                return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
            }
        }
    }

    public void StopEffect(string _soundName) // ȿ���� ���� �޼ҵ�
    {
        if (_soundName == null) // ���� �̸��� �������� �ʴٸ�
        {
            return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
        }

        for (int i = 0; i < effect.Length; ++i) // ȿ���� ũ�⸦ ������ �迭�� ũ�⸸ŭ �ݺ����� �����Ѵ�
        {
            if (playSoundName[i] != null && playSoundName[i].Equals(_soundName) == true)
                // �������� ���� �̸��� ���ڷ� �޾ƿ� ���� �̸��� �����ϸ�
            {
                effect[i].Stop(); // ȿ���� ���� �޼ҵ带 ȣ���Ѵ�
                effect[i].clip = null; // ȿ���� Ŭ���� �η���� ����� ���ش�
                playSoundName[i] = null; // �������� ���� �̸��� �η���� ����� ���ش�
            }
        }
    }

    public void StopAllEffect() // ��� ȿ���� ���� �޼ҵ�
    {
        for (int i = 0; i < effect.Length; ++i) // ȿ���� ũ�⸦ ������ �迭�� ũ�⸸ŭ �ݺ����� �����Ѵ�
        {
            effect[i].Stop(); // ȿ���� ���� �޼ҵ带 ȣ���Ѵ�
            effect[i].clip = null; // ȿ���� Ŭ���� �η���� ����� ���ش�
            playSoundName[i] = null; // �������� ���� �̸��� �η���� ����� ���ش�
        }
    }

    public void PlayBackground(string _soundName) // ����� ���� �޼ҵ�
    {
        if (_soundName == null) // ���� �̸��� �������� �ʴٸ�
        {
            return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
        }

        if (_soundName.Equals(backgroundSound.name) == true) // ���� ������ �޾ƿ� ���� �̸��� ����� �̸��� ������
        {
            background.clip = backgroundSound.clip; // ����� ���� Ŭ���� ����� Ŭ������ �����Ѵ�
            background.Play(); // ����� ����
        }
    }

    public void StopBackground(string _soundName) // ����� ���� �޼ҵ�
    {
        if (_soundName == null) // ���� �̸��� �������� �ʴٸ�
        {
            return; // �Ʒ� ���� ��ɾ���� �����ϰ� ��ȯ
        }

        if (background.isPlaying == true && _soundName.Equals(backgroundSound.name) == true)
            // ������� ���� ���̰� ���ڰ����� �޾ƿ� ���� �̸��� ����� ���� �̸��� �����ϸ�
        {
            background.Stop(); // ����� ���� �޼ҵ� ȣ��
        }
    }
}
