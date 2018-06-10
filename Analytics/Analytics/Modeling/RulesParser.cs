using Analytics.Modeling;
using Analytics.Modeling.ModelingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class RulesParser
    {
        public void go_parse(ModelingModel model)
        {
            CheinPartOfOperationCreator advanceOperation = new CheinPartOfOperationCreator(
                new AdvanceOperation(model));
            CheinPartOfOperationCreator assignOperation = new CheinPartOfOperationCreator(
                new AssignOperation(model));
            CheinPartOfOperationCreator departOperation = new CheinPartOfOperationCreator(
                new DepartOperation(model));
            CheinPartOfOperationCreator queueOperation = new CheinPartOfOperationCreator(
                new QueueOperation(model));
            CheinPartOfOperationCreator releaseOperation = new CheinPartOfOperationCreator(
                new ReleaseOperation(model));
            CheinPartOfOperationCreator savevalueOperation = new CheinPartOfOperationCreator(
                new SavevalueOperation(model));
            CheinPartOfOperationCreator seizeOperation = new CheinPartOfOperationCreator(
                new SeizeOperation(model));
            CheinPartOfOperationCreator terminateOperation = new CheinPartOfOperationCreator(
                new TerminateOperation(model));
            CheinPartOfOperationCreator testOperation = new CheinPartOfOperationCreator(
                new TestOperation(model));
            CheinPartOfOperationCreator transferOperation = new CheinPartOfOperationCreator(
                new TransferOperation(model));
            CheinPartOfOperationCreator initialOperation = new CheinPartOfOperationCreator(
                new InitialOperation(model));
            CheinPartOfOperationCreator variableOperation = new CheinPartOfOperationCreator(
                new VariableOperation(model));
            CheinPartOfOperationCreator generateOperation = new CheinPartOfOperationCreator(
                new GenerateOperation(model));
            CheinPartOfOperationCreator storageOperation = new CheinPartOfOperationCreator(
                new StorageOperation(model));
            CheinPartOfOperationCreator enterOperation = new CheinPartOfOperationCreator(
                new EnterOperation(model));
            CheinPartOfOperationCreator leaveOperation = new CheinPartOfOperationCreator(
                new LeaveOperation(model));


            advanceOperation.setNext(assignOperation);
            assignOperation.setNext(departOperation);
            departOperation.setNext(queueOperation);
            queueOperation.setNext(releaseOperation);
            releaseOperation.setNext(savevalueOperation);
            savevalueOperation.setNext(seizeOperation);
            seizeOperation.setNext(terminateOperation);
            terminateOperation.setNext(testOperation);
            testOperation.setNext(transferOperation);
            transferOperation.setNext(initialOperation);
            initialOperation.setNext(variableOperation);
            variableOperation.setNext(generateOperation);
            generateOperation.setNext(storageOperation);
            storageOperation.setNext(enterOperation);
            enterOperation.setNext(leaveOperation);


            ModelingState modeling_objects = new ModelingState();
            //перебор каждой строк и поиск в ней определения какого-либо объекта
            for (int i = 0; i < model.getState().originalRules.Count; i++)
            {
                model.getState().newRules.Add(advanceOperation.processing(model.getState().originalRules.
                    ElementAt(i)));
            }
            int idfd = 0;
        }
    }
}
