import { AfterViewInit, Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Todo } from '../../models/todo';
import { FormsModule } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Router } from '@angular/router';
import { Priority } from '../../models/priority.enum'
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';
import { TodoService } from '../../services/todo.service';
import { MatBadgeModule } from '@angular/material/badge'
import { ToastrService } from 'ngx-toastr';
import { SHARED_MATERIAL_PROVIDERS } from '../shared/MaterialImports/shared.materials';

@Component({
  selector: 'app-task-table',
  standalone: true,
  imports: [CommonModule,
    MatTableModule,
    MatCheckboxModule,
    FormsModule,
    DatePipe,
    MatPaginator,
    MatSortModule,
    MatBadgeModule,
    SHARED_MATERIAL_PROVIDERS
  ],
  templateUrl: './todo-table.component.html',
  styleUrl: './todo-table.component.css'
})
export class TodoTableComponent implements AfterViewInit, OnChanges {

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private service: TodoService,
    private toastr: ToastrService,
  ) { }

  @Input() todoItems: Todo[] = [];
  @Input() totalUsers: number = 0;
  @Input() pageSize: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  Priority = Priority;
  dataSource = new MatTableDataSource<Todo>();
  displayedColumns: string[] = ['name', 'startDate', 'endDate', 'priority', 'actions'];

  ngOnChanges(changes: SimpleChanges) {
    if (changes['todoItems'] && this.todoItems && this.todoItems.length) {

      this.dataSource = new MatTableDataSource(this.todoItems);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;

    }
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  toggleRowSelection(todo: Todo) {
    todo.isSelected = !todo.isSelected;
  }

  editTask(id: string) {
    this.router.navigate(['/edit', id])
  }

  loadTodoItems() {
    this.service.getTodoItems(0, this.pageSize)
      .subscribe({
        next: (data) => {
          this.service.todoItemList = [...data.todoItems];
          this.totalUsers = data.total;
        },
        error: (err) => {
          this.toastr.error(`Unable to get todo Items ${err.error.detail}`, 'Todo Detail register');
        }
      });
  }

  onDelete(id: string) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: { message: 'Are you sure you want to delete this task?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.deleteTodoItem(id).subscribe({
          next: resp => {
            this.loadTodoItems();
          },
          error: err => {
            this.toastr.error(`Unable to get delete todo Item ${id} ${err.error.detail}`, 'Todo Detail register');
          }
        });
      }
    });
  }

  goToAdd() {
    this.router.navigate(['/add']);
  }

}
