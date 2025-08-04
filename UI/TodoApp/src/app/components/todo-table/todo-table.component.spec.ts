import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TodoTableComponent } from './todo-table.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { Todo } from '../../models/todo';
import { TodoService } from '../../services/todo.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';


describe('TodoTableComponent', () => {
  let component: TodoTableComponent;
  let fixture: ComponentFixture<TodoTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TodoTableComponent, NoopAnimationsModule],
      providers:[
         provideHttpClientTesting(),
        { provide: TodoService, useValue: {} },
        { provide: MatDialog, useValue: {} },
        { provide: ToastrService, useValue: { success: () => {}, error: () => {} } }
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TodoTableComponent);
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
    const totalUsers = 1;

    component.dataSource.data = todoItems;
    fixture.detectChanges();

    const rows = fixture.nativeElement.querySelectorAll('mat-row');
    debugger; 
    expect(rows.length).toBe(1);
  });

  it('should render actions items on the table', () => {
    const todoItems:Todo[] = [
      { id: '1', name: 'Test', description:'Test', startDate: new Date('2025-08-10') , endDate: new Date('2025-08-12'), priority: 1, isSelected:true},
    ];
    component.dataSource.data = todoItems;
    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;

    // Query all mat-icon elements
    const icons = compiled.querySelectorAll('mat-icon');

    // Check that edit and delete icons are present
    const iconTexts = Array.from(icons).map(icon => icon.textContent?.trim());
    expect(iconTexts).toContain('edit');
    expect(iconTexts).toContain('delete');
  });

});
