import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodoListComponent } from './todo-list.component';
import { Todo } from '../../models/todo';

describe('TodoListComponent', () => {
  let component: TodoListComponent;
  let fixture: ComponentFixture<TodoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TodoListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TodoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render todo items in the table', () => {
    const todoItems:Todo[] = [
      { id: '1', name: 'Test', description:'Test', startDate: new Date('2025-08-10') , endDate: new Date('2025-08-12'), priority: 1, isSelected:true},
    ];
    component.service.todoItemList = todoItems;
    fixture.detectChanges();

    const rows = fixture.nativeElement.querySelectorAll('table mat-row');
    expect(rows.length).toBe(1);
  });


});
