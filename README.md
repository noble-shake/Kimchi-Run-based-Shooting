# Kimchi Run Shooting

Unity 6 Challenge의 Kimchi Run 예제를 바탕으로, 달리기 게임을 탄막 슈팅으로 바꿔 만든 작품입니다.

취업을 준비하며 공부용으로 만든 프로젝트입니다.

- 엔진: Unity 6 (6000.0.25f1)
- 개발 시기: 2025년 1월

## 이 저장소에 들어 있는 것

**직접 작성한 코드만** 들어 있습니다. 그림·소리 리소스와 에셋 스토어 팩은 포함하지 않았습니다.
그래서 이 저장소만 받아서는 프로젝트를 열어 실행할 수 없습니다.

| 경로 | 내용 |
|---|---|
| `kimchiRun/Assets/Scripts/` | 게임 코드 전체 |
| `kimchiRun/Packages/manifest.json` | 사용한 Unity 패키지 목록 |
| `kimchiRun/ProjectSettings/ProjectVersion.txt` | 엔진 버전 |

## 구현한 것

- **이동과 공격의 분리**: 적의 이동(`MoveCommand`)과 공격(`AttackCommand`)을 각각 인터페이스로 나누고, 구현체를 조합해 적을 구성
  - 이동: 직선 하강, 좌·우 사선 하강 (`MCB_*`)
  - 공격: 조준탄, 3방향 조준, 원형 확산 (`ACB_*`)
- **보스**: 이동 패턴 3종과 공격 패턴 3종을 전환 (`Boss_MCB_*`, `Boss_ACB_*`)
- **시퀀서**: 코루틴을 대기열에 쌓아 차례로 실행해 등장 연출과 진행 순서를 제어 (`Sequences.cs`)
- 아이템, 보조 탄, 배경 스크롤

## 코드 구조

```
kimchiRun/Assets/Scripts/
├── Enemy/          적, 보스, 이동·공격 패턴, 적 탄
├── UI/             타이틀과 화면 UI
├── PlayerScript.cs, BulletScript*.cs
├── Sequences.cs    연출 순서 제어
└── GameManager.cs, ItemManager.cs, AudioManager.cs
```

## 사용한 외부 에셋 (미포함)

Fantasy Skybox FREE
