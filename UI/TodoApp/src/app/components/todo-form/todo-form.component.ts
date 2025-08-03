import { Component,OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCard } from '@angular/material/card';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Priority } from '../../models/priority.enum';

@Component({
  selector: 'app-todo-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatButtonModule,
    MatCard,
    MatProgressSpinnerModule
  ],
  templateUrl: './todo-form.component.html',
  styleUrl: './todo-form.component.css'
})
export class TodoFormComponent implements OnInit {
  Priority=  Priority;
  constructor(public service: TodoService, 
  private toastr:ToastrService,
  private activatedRoute: ActivatedRoute,
  private router:Router
  ){}

  loading:boolean = false;
  
  ngOnInit(): void {
    const id =  this.activatedRoute.snapshot.paramMap.get('id') ?? "";
    this.loadTodoItem(id);
  }
  
  
  onSubmit(form:NgForm){
    this.loading = true;
    this.service.formSubmitted = true;
  
    if(form.valid){
      if(this.service.formData.id !== "")
      {
        this.service.updateTodoItem()
        .subscribe({
             next:resp => {
              this.loading = false;
               //reset the form
               this.service.resetForm(form);
               //toast message
               this.toastr.success('TodoItem updated successfully', 'Todo Detail register')
               //go back to the list page
               this.goBack(form);
             },
             error:err => {
               this.loading = false;
               if(err?.error.title === "ValidationException"){
                  this.toastr.error(`Validation Error: ${err?.error.detail}`, 'Todo Detail register',{ disableTimeOut:true, enableHtml:true, toastClass:'custom-toastr' });
               }else{
                  this.toastr.error(`TodoItem updated unsuccessfull ${err.error.detail}`, 'Todo Detail register');
               }
             }
        })
      }else{
      // call the service and reset the form 
      const { id, ...formValue} = this.service.formData;
      this.service.postTodoItem(formValue)
        .subscribe({
             next:resp => {
               this.loading = false;
               //reset the form
               this.service.resetForm(form);
               //toast message
               this.toastr.success('Task added successfully', 'Todo Detail register')
               //go back to the list page
               this.goBack(form);
             },
             error:err => {
               this.loading= false;
               if(err?.error.title === "ValidationException"){
                  this.toastr.error(`Validation Error: ${err?.error.detail}`, 'Todo Detail register',{disableTimeOut:true, enableHtml:true });
               }else{
                  this.toastr.error(`Adding Todo Item unsuccessfull ${err.error.detail}`, 'Todo Detail register');
               }
             }
        })
      
      }}else{
        this.loading= false;
      }
    }
  
    loadTodoItem(id:string){
      const todoItem = this.service.todoItemList.find(t => t.id === id);
      if(todoItem){
        this.service.formData = {
          ...todoItem,
          priority : todoItem.priority as Priority
        };
      }
    }
  
    onCancel(form:NgForm){
      this.service.resetForm(form);
    }
  
    goBack(form:NgForm){
      this.service.resetForm(form);
      this.router.navigate(['/todos']);
    }
}

