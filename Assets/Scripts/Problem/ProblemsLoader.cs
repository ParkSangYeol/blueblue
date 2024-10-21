using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Problem
{
    public class ProblemsLoader : MonoBehaviour
    {
        public ProblemMapHandler beforeProblemMap;
        public ProblemMapHandler currentProblemMap;
        public ProblemMapHandler nextProblemMap;
        public List<StageScriptableObject> stages;
        public int stageThreshold;
        private int[,] numOfAppearance; // [스테이지][문제 번호]의 등장 횟수 추적.
        private int currentMapIdx;
        private int minAppearance;
        private int stageIdx;
        private int stageFloor;
        private int sequenceProblem;
        private bool isLoadNewMap = false;

        private void Awake()
        {
            if (stageThreshold == 0)
            {
                stageThreshold = 4;
            }

            minAppearance = 0;
            stageIdx = 0;
            stageFloor = 1;
            sequenceProblem = 0;
            currentMapIdx = -1;
            int maxNumOfProblems = 0;
            for (int i = 0; i < stages.Count; i++)
            {
                maxNumOfProblems= Math.Max(maxNumOfProblems, stages[i].problems.Count);
            }
            numOfAppearance = new int[stages.Count, maxNumOfProblems];
        }
        
        [Button]
        public void MakeChoice(bool playerChoice)
        {
            // 왼쪽은 이상현상이 있다는 판단, 오른쪽은 없다는 판단이라고 보면 
            // 정상 맵일 때 왼쪽은 오답, 오른쪽은 정답
            // 이상현상 맵일 때 왼쪽은 정답, 오른쪽은 오답.
            // playerChoice는 이상현상이 있는지 없는지에 대한 선택을 의미.
            if (isLoadNewMap)
            {
                return;
            }
            isLoadNewMap = true;
            
            bool isAnswer = currentMapIdx == -1 ? !playerChoice : playerChoice;
            if (isAnswer)
            {
                //정답을 맞춘경우
                if (currentMapIdx != -1)
                {
                    numOfAppearance[stageIdx , currentMapIdx]++;
                }
                if (++stageFloor == stageThreshold)
                {
                    // 다음 스테이지 로드
                    minAppearance = 0;
                    if (++stageIdx == stages.Count)
                    {
                        // TODO 엔딩 출력
                    }
                }
            }
            else
            {
                // 틀린 경우
                if (stageFloor > 1)
                {
                    stageFloor = 1;
                }
                else if (stageFloor == 1)
                {
                    stageFloor = 0;
                }
                else if (stageFloor == 0)
                {
                    // TODO 배드엔딩 출력
                }
            }

            if (sequenceProblem == 2)
            {
                // 연속 정상 맵 등장
                LoadProblem(GetRandomProblem(), playerChoice);
                sequenceProblem = 0;
                return;
            }

            float prob = Random.Range(0f, 1f);
            if (prob < 0.4f)
            {
                // 정상 챕터 사용
                sequenceProblem++;
                currentMapIdx = -1;
                LoadProblem(stages[stageIdx].defaultPrefab, playerChoice);
            }
            else
            {
                sequenceProblem = 0;
                LoadProblem(GetRandomProblem(), playerChoice);
            }
        }
        
        public void LoadProblem(ProblemScriptableObject problemData, bool isLeft)
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.Log("현재 게임 뷰가 실행 상태가 아닙니다. 실행 상태에서 이 함수를 호출해 주세요.");
                return;
            }
            
            // 이상현상 생성. (혹은 가져오기)
            Transform spawnTransform = currentProblemMap.loadTransform;
            GameObject problemMapObject = Instantiate(problemData.problemPrefab, spawnTransform.position, spawnTransform.rotation);
            nextProblemMap = problemMapObject.GetComponent<ProblemMapHandler>();
            // 이상현상 리셋.
            nextProblemMap.ResetProblem();
            nextProblemMap.choiceTrueCollider.loader = nextProblemMap.choiceFalseCollider.loader = nextProblemMap.unloadCollider.loader = this;
            
            // 문 여는 애니메이션 실행.
            if (isLeft)
            {
                currentProblemMap.leftDoor.OpenDoor();
            }
            else
            {
                currentProblemMap.rightDoor.OpenDoor();
            }

        }
        
        public void LoadProblem(GameObject problemMap, bool isLeft)
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.Log("현재 게임 뷰가 실행 상태가 아닙니다. 실행 상태에서 이 함수를 호출해 주세요.");
                return;
            }
            
            // 이상현상 생성. (혹은 가져오기)
            Transform spawnTransform = currentProblemMap.loadTransform;
            GameObject problemMapObject = Instantiate(problemMap, spawnTransform.position, spawnTransform.rotation);
            nextProblemMap = problemMapObject.GetComponent<ProblemMapHandler>();
            // 이상현상 리셋.
            nextProblemMap.ResetProblem();
            nextProblemMap.choiceTrueCollider.loader = nextProblemMap.choiceFalseCollider.loader = nextProblemMap.unloadCollider.loader = this;
            
            // 문 여는 애니메이션 실행.
            if (isLeft)
            {
                currentProblemMap.leftDoor.OpenDoor();
            }
            else
            {
                currentProblemMap.rightDoor.OpenDoor();
            }
        }
        
        [Button]
        public void UnloadProblem()
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.Log("현재 게임 뷰가 실행 상태가 아닙니다. 실행 상태에서 이 함수를 호출해 주세요.");
                return;
            }
            if (!isLoadNewMap)
            {
                return;
            }
            
            // 문을 닫는 애니메이션 실행
            /*
            if (isLeft)
            {
                currentProblemMap.leftDoor.CloseDoor();
            }
            else
            {
                currentProblemMap.rightDoor.CloseDoor();
            }
            */
            
            // 필요 없어진 이상현상 오브젝트 제거.
            if (beforeProblemMap != null)
            {
                Destroy(beforeProblemMap.gameObject);
            }
            
            beforeProblemMap = currentProblemMap;
            currentProblemMap = nextProblemMap;
            isLoadNewMap = false;
        }
        
        private ProblemScriptableObject GetRandomProblem()
        {
            // 현재 데이터 중 등장하지 않은 랜덤한 값 출력.
            List<int> idxs = new List<int>();
            int tempMinAppearance = 987654321;
            int minCount = 0;
            for (int i = 0; i < stages[stageIdx].problems.Count; i++)
            {
                if (tempMinAppearance > numOfAppearance[stageIdx, i])
                {
                    tempMinAppearance = numOfAppearance[stageIdx, i];
                    minCount = 0;
                }
                else if (tempMinAppearance == numOfAppearance[stageIdx, i])
                {
                    minCount++;
                }
                
                if (numOfAppearance[stageIdx, i] <= minAppearance)
                {
                    idxs.Add(i);
                }
            }
            minAppearance = minCount == 1? tempMinAppearance +1 : tempMinAppearance;
            int randIdx = Random.Range(0, idxs.Count);
            currentMapIdx = idxs[randIdx];
            ProblemScriptableObject returnData = stages[stageIdx].problems[idxs[randIdx]];
            return returnData;
        }
    }

}