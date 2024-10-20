# AppleTreeDefense

## 최적화
#### 1. Object Pooling을 통한 최적화 - UnityEngine.Pool 사용 [Tree.cs](https://github.com/SeongWook-Jo/AppleTreeDefense/blob/master/Assets/Scripts/Scene/Stage/Tree.cs)
#### 2. Atlas를 통한 드로우 콜 최적화 - [ResourceManager](https://github.com/SeongWook-Jo/AppleTreeDefense/blob/master/Assets/Scripts/Managers/ResourceManager.cs)

## 구조
#### 1. Singleton을 통한 유저 정보 전역 관리 - [Player.cs](https://github.com/SeongWook-Jo/AppleTreeDefense/blob/master/Assets/Scripts/Instance/Player.cs)
#### 2. CSV를 통한 게임 데이터 로드 - [InfoManager.cs](https://github.com/SeongWook-Jo/AppleTreeDefense/blob/master/Assets/Scripts/Info/InfoManager.cs)
#### 3. 확장 메소드를 통한 간편화 및 가독성 향상 - [Extensions.cs](https://github.com/SeongWook-Jo/AppleTreeDefense/blob/master/Assets/Scripts/Managers/Extensions.cs)