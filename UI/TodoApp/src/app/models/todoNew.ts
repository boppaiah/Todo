import { Priority } from '../models/priority.enum';

export interface TodoNew {
    name: string;
    description: string | null;
    startDate : Date | null;
    endDate :Date | null;
    priority: Priority;
    isSelected: boolean;
}
