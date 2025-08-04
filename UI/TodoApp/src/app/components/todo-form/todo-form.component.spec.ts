import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TodoService } from '../../services/todo.service';
import { ToastrService } from 'ngx-toastr';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { TodoFormComponent } from './todo-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Priority } from '../../models/priority.enum';


describe('TodoFormComponent', () => {
  let component: TodoFormComponent;
  let fixture: ComponentFixture<TodoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TodoFormComponent, FormsModule, NoopAnimationsModule],
      providers:[
         provideHttpClientTesting(),
        { provide: TodoService, useValue: { todoItemList: [{id:"", name:"", isSelected:false, priority:Priority.Low, endDate:null, startDate:null, description:null}] } },
        { provide: ActivatedRoute, useValue: { snapshot: {paramMap: {get: () => null}}} },
        { provide: Router, useValue: {navigate: () => {}} },
        { provide: ToastrService, useValue: { success: () => {}, error: () => {} } }
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TodoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should mark form as invalid when required fields are missing', () => {
    const form = fixture.nativeElement.querySelector('form');
    expect(form.checkValidity()).toBeFalse();
  });
  
});