import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http"
import { environment } from '../../environments/environment';
import { Todo } from '../models/todo';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { Priority } from '../models/priority.enum';
import { TodoNew } from '../models/todoNew';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  url:string = environment.apiBaseUrl;
  todoItemList: Todo[] = [];
  formData: Todo={id:"", name:"", isSelected:false, priority:Priority.Low, endDate:null, startDate:null, description:null};
  formSubmitted:boolean = false;

  constructor(private http:HttpClient) { }

  getTodoItems(pageIndex: number, pageSize: number): Observable<{ todoItems: Todo[]; total: number }> {
    const params = new HttpParams()
      .set('pageNumber', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<{ todoItems: Todo[]; total: number }>(this.url, {params})

  }

  postTodoItem(formWithoutId:TodoNew){
    return this.http.post(this.url,formWithoutId);
  }

  updateTodoItem(){
    return this.http.put(this.url,this.formData);
  }

  deleteTodoItem(id:string):  Observable<{ isSuccess:boolean }>{
    const params = new HttpParams()
      .set('id', id);
    return this.http.delete<{isSuccess:boolean}>(this.url,{params});
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.formData = {id:"", name:"", isSelected:false, priority:Priority.Low, endDate:null, startDate:null, description:null};
    this.formSubmitted = false;
  }
}
