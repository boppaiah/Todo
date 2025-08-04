import { TestBed } from '@angular/core/testing';
import {  HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TodoService } from './todo.service';
import { Todo } from '../models/todo';
import { HttpClient } from '@angular/common/http';
import { TodoNew } from '../models/todoNew';


describe('TodoService', () => {
  let service: TodoService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TodoService]
    });

    service = TestBed.inject(TodoService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should retrieve todos via GET', () => {
    const mockResponse = {
      todoItems: [
        { id: '1', name: 'Test Todo', description: 'Test Desc', startDate: new Date(), endDate: new Date(), priority: 1, isSelected: false }
      ],
      total: 1
    };

    service.getTodoItems(1,5).subscribe(todos => {
      expect(todos.todoItems.length).toBe(1);
      expect(todos.todoItems).toEqual(mockResponse.todoItems);
    });

    const req = httpMock.expectOne('https://localhost:7128/api/v1/todo/items?pageNumber=1&pageSize=5'); // change to your actual endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should post a new todo via POST', () => {
    const newTodo: TodoNew = {
      name: 'New Todo',
      description: 'new',
      startDate: new Date(),
      endDate: new Date(),
      priority: 2,
      isSelected: false
    };

    service.postTodoItem(newTodo).subscribe(todo => {
      expect(todo).toEqual(newTodo);
    });

    const req = httpMock.expectOne('https://localhost:7128/api/v1/todo'); // update URL as needed
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newTodo);
    req.flush(newTodo);
  });
});