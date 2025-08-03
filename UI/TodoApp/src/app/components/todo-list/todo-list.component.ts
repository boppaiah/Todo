import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { DatePipe } from '@angular/common';
import { Todo } from '../../models/todo';
import { TaskTableComponent } from '../task-table/task-table.component';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [TaskTableComponent ],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})
export class TodoListComponent implements OnInit  {
 
  constructor(
    public service: TodoService,
    private toastr:ToastrService,
  ){}
 
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
        this.toastr.error(`Unable to connect to server ${err.message}`, 'Todo Detail register');
      },
    });
  }

  toggleRowSelection(task: Todo){
    console.log(task.isSelected);
    //send a patch request to update the list item by calling the service
  }


}
