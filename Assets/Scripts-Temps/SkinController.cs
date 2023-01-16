using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftsmanHero {
    public class SkinController : MonoBehaviour {
        public List<SkinsSO> Skins;

        private int currentSkin;
        private int currentState;
        private int currentFrame;

        [SerializeField]
        SkinsSO skin;
        private Sprite[,] spriteCache;

        SpriteRenderer sr;

        private void Awake() {
            sr = GetComponent<SpriteRenderer>();
            currentState = 2;
            LoadFrame();
            ChangeSkin(0);
        }

        // ��ͼƬ�и���棬֮�󲻻��������
        public void LoadFrame() {
            skin = Skins[currentSkin];
            spriteCache = new Sprite[skin.SkinsStatus.Count, skin.SkinsStatus.Max(status => status.FrameLength)];
            for (int state = 0; state < skin.SkinsStatus.Count; state++) {
                for (int frame = 0; frame < skin.SkinsStatus[state].FrameLength; frame++) {
                    Rect rect = new(
                        frame * skin.FrameSize.x, // �޸� x ֵ�ı�֡��
                        state * skin.FrameSize.y, // �޸� y ֵ�ı䶯�����������ϣ�
                        skin.FrameSize.x,
                        skin.FrameSize.y);
                    Sprite skinSprite = Sprite.Create(skin.SkinSprite, rect, skin.Pivot, 16f);
                    spriteCache[state, frame] = skinSprite;
                }
            }
        }

        public void NextFrame() {
            currentFrame = (++currentFrame) % skin.SkinsStatus[currentState].FrameLength;
            sr.sprite = spriteCache[currentState, currentFrame];
        }

        public void ChangeSkin(int skinIdx) {
            CancelInvoke();
            currentSkin = skinIdx;
            LoadFrame();
            InvokeRepeating(nameof(NextFrame), 0f, 1f / skin.sampleRate);
        }

        public void ChangeSkin(string skinId) {
            CancelInvoke();
            currentSkin = Skins.IndexOf(Skins.Find(x => x.SkinID.Equals(skinId)));
            LoadFrame();
            InvokeRepeating(nameof(NextFrame), 0f, 1f / skin.sampleRate);
        }
    }
}
