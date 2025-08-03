import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { DatePipe } from '@angular/common';
import { Todo } from '../../models/todo';
import { TaskTableComponent } from '../task-table/task-table.component';


@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [TaskTableComponent ],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})
export class TodoListComponent implements OnInit  {
 constructor(public service: TodoService){}
 
  totalUsers = 0;
  initialPageSize = 5;
  initialPageIndex = 0;
 //initial render
 ngOnInit(): void {

    this.loadTasks(this.initialPageIndex, this.initialPageSize);
  }

  loadTasks(pageIndex:number, pageSize:number){
  this.service.getTodoItems(pageIndex, pageSize)
    .subscribe({
       next: (data) => {
        this.service.todoItemList = data.todoItems;
        this.totalUsers = data.total;
      },
      error: (err) => {
                this.service.todoItemList = [{
                  id:"123",
                  name:"test",
                  description:"test",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"1213",
                  name:"test2",
                  description:"test2",
                  priority:3,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"2123",
                  name:"test3",
                  description:"test3",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"1233",
                  name:"test4",
                  description:"test4",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"12d3",
                  name:"test5",
                  description:"test5",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"454",
                  name:"test6",
                  description:"test6",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"342",
                  name:"test7",
                  description:"test7",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"674",
                  name:"test8",
                  description:"test8",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"65463",
                  name:"test9",
                  description:"test9",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"5363",
                  name:"test10",
                  description:"test10",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                },
              {
                  id:"12123",
                  name:"test11",
                  description:"test11",
                  priority:1,
                  endDate:new Date(),
                  startDate: new Date(),
                  isSelected:false
                }]

                ;
        this.totalUsers = 11;
      },
    });
  }

  toggleRowSelection(task: Todo){
    console.log(task.isSelected);

    //send a patch request to update the list item by calling the service
  }

  // populateForm(selectedTask:Todo){
  //   this.service.formData = {
  //     ...selectedTask,
  //     startDate: this.datePipe.transform(selectedTask.startDate, 'yyyy-MM-dd')|| '' ,
  //     endDate: this.datePipe.transform(selectedTask.endDate, 'yyyy-MM-dd')|| '',
  //   };
  // }

}
