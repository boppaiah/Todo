import { Routes } from '@angular/router';

export const routes: Routes = [
    {path:'', redirectTo:'todos', pathMatch:'full'},
    {
        path:'todos',
        loadComponent:()=>
            import('./components/todo-list/todo-list.component').then(m => m.TodoListComponent)
        
    },
    {
        path:'add',
        loadComponent:()=>
            import('./components/todo-form/todo-form.component').then(m => m.TodoFormComponent)
        
    },
    {
        path:'edit/:id',
        loadComponent:()=>
            import('./components/todo-form/todo-form.component').then(m => m.TodoFormComponent)
        
    }

];
