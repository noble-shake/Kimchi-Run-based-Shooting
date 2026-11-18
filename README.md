# Kimchi Run Shooting

Unity 6 Challenge의 Kimchi Run 예제를 바탕으로, 달리기 게임을 탄막 슈팅으로 바꿔 만든 작품입니다.

취업을 준비하며 공부용으로 만든 프로젝트입니다.

- 엔진: Unity 6 (6000.0.25f1)
- 개발 시기: 2025년 1월

## 원본 저장소

이 저장소는 공개용입니다. 에셋을 포함한 전체 프로젝트 원본은 비공개 저장소 `Kimchi-Run-based-Shooting-Archive`에 따로 보관하고 있습니다.

원본에는 에셋 스토어에서 받은 팩과 그림·소리 리소스가 들어 있어 라이선스상 공개할 수 없습니다. 그래서 여기에는 **직접 작성한 코드만** 옮겼고, 이 저장소만 받아서는 프로젝트를 열어 실행할 수 없습니다.

## 빌드 다운로드

실행 파일은 이 저장소의 [Releases](https://github.com/noble-shake/Kimchi-Run-based-Shooting/releases)에 올립니다.

현재는 준비 중이며, 올라오면 Releases 페이지에서 받을 수 있습니다.

## 폴더 구조

```
Kimchi-Run-based-Shooting/
└── kimchiRun/                Unity 프로젝트 폴더
    ├── Assets/
    │   └── Scripts/          게임 코드 전체
    │       ├── Enemy/        적, 보스, 이동·공격 패턴, 적 탄
    │       ├── UI/           타이틀과 화면 UI
    │       ├── PlayerScript.cs, BulletScript*.cs
    │       ├── Sequences.cs  연출 순서 제어
    │       └── GameManager.cs, ItemManager.cs, AudioManager.cs
    ├── Packages/manifest.json    사용한 Unity 패키지 목록
    └── ProjectSettings/ProjectVersion.txt   엔진 버전
```

## 구현한 것

- **이동과 공격의 분리**: 적의 이동(`MoveCommand`)과 공격(`AttackCommand`)을 각각 인터페이스로 나누고, 구현체를 조합해 적을 구성
  - 이동: 직선 하강, 좌·우 사선 하강 (`MCB_*`)
  - 공격: 조준탄, 3방향 조준, 원형 확산 (`ACB_*`)
- **보스**: 이동 패턴 3종과 공격 패턴 3종을 전환 (`Boss_MCB_*`, `Boss_ACB_*`)
- **시퀀서**: 코루틴을 대기열에 쌓아 차례로 실행해 등장 연출과 진행 순서를 제어 (`Sequences.cs`)
- 아이템, 보조 탄, 배경 스크롤

## 사용한 외부 에셋 (미포함)

Fantasy Skybox FREE
