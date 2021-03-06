type MessageType = 'success' | 'error' | 'warn' | 'info';

export interface Messages {
  messages: string[];
  messageType: MessageType;
}
