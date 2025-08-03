import { Priority } from '../models/priority.enum';

export interface Todo {
    id: string;
    name: string;
    description: string | null;
    startDate : Date | null;
    endDate :Date | null;
    priority: Priority;
    isSelected: boolean;
}

